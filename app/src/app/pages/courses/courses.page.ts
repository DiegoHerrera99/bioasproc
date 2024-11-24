import { Component, OnInit } from '@angular/core';
import { ModalController, Platform } from '@ionic/angular';
import { VideoPlayerComponent } from 'src/app/components/video-player/video-player.component';
import { CoursesService } from 'src/app/services/courses/courses.service';
import { PdfsService } from 'src/app/services/pdfs/pdfs.service';
import { VideosService } from 'src/app/services/videos/videos.service';
import { CourseGetDto } from 'src/models/courses/CourseGetDto';
import { Directory, Filesystem, WriteFileResult } from '@capacitor/filesystem';
import { FileOpener } from '@capacitor-community/file-opener';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.page.html',
  styleUrls: ['./courses.page.scss'],
})
export class CoursesPage implements OnInit {
  public courses: CourseGetDto[] = [];
  public loading: boolean = false;
  public error: boolean = false;
  public errorBtns: string[] = ['Intentar de nuevo'];

  constructor(
    private platform: Platform,
    private modalCtrl: ModalController,
    private coursesService: CoursesService,
    private videosService: VideosService,
    private pdfsService: PdfsService
  ) { }

  ngOnInit() {
    this.setCourses();
  }

  public playVideo(courseId: number, name: string, description: string): void {
    this.setLoading(true);
    this.videosService.getStream(courseId).subscribe({
      next: (res) => {
        this.setLoading(false);
        if (res.status != 200) {
          this.setError(true);
          return;
        }

        this.openVideoPlayer(res.cdnUrl, name, description);
      },
      error: (error) => {
        console.log('ERROR', error);
        this.setLoading(false);
        this.setError(true);
      },
      complete: () => {
        this.setLoading(false);
      }
    });
  }

  private async openVideoPlayer(cdn: string, name: string, description: string, ): Promise<void> {
    const modal = await this.modalCtrl.create({
      component: VideoPlayerComponent,
      componentProps: { 
        cdn: 'https://' + cdn,
        description,
        name
      },
    });

    modal.present();
  }

  public downloadCoursePdf(courseId: number) {
    try {
      this.setLoading(true);
      this.pdfsService.getPdf(courseId).subscribe({
        next: async (blob) => {
          if (this.platform.is('capacitor')) {
            const fileName = this.getCourseName(courseId).replace(/\s+/g, '-') + '.pdf';
            const base64Data = await this.pdfsService.convertBlobToBase64(blob);
    
            const savedFile: WriteFileResult = await Filesystem.writeFile({
              path: fileName,
              data: base64Data,
              directory: Directory.Documents,
              recursive: true,
            });
            
            await FileOpener.open({
              filePath: savedFile.uri,
              contentType: 'application/pdf',
            });

          } else {
            // Para web
            const url = URL.createObjectURL(blob);
            window.open(url, '_blank');
            URL.revokeObjectURL(url);
          }

          this.setLoading(false);
        },
        error: (error) => {
          this.setLoading(false);
          throw new Error(`Ocurrio un error al descargar el PDF ${error}`);
        },
        complete: () => {
          this.setLoading(false);
        }
      });
    }
    catch (err) {
      console.log((err as Error).message);
      this.setError(true);
    }
  }

  private getCourseName(courseId: number): string {
    let [ _course ] = this.courses.filter(c => c.courseId === courseId);
    if (_course !== null) return _course.name;
    return  '';
  }

  private setCourses(): void {
    this.setLoading(true);
    this.coursesService.getCoursesList().subscribe({
      next: (res) => {
        this.courses = res.map(c => {
          if (c.imgPath != null && c.imgPath != '') c.imgPath = 'https://' + c.imgPath;
          return c;
        });
        
        this.setLoading(false);
      },
      error: (error) => {
        console.log('ERROR', error);
        this.setLoading(false);
        this.setError(true);
      },
      complete: () => {
        this.setLoading(false);
      }
    });
  }

  private setLoading(flg: boolean) {
    this.loading = flg;
  }

  public setError(flg: boolean) {
    if (!flg) this.setCourses();
    this.error = flg;
  }
}
