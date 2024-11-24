import { Component, OnInit } from '@angular/core';
import { FileOpener } from '@capacitor-community/file-opener';
import { Directory, Filesystem, WriteFileResult } from '@capacitor/filesystem';
import { Platform } from '@ionic/angular';
import { CertificationInformationService } from 'src/app/services/certification-information/certification-information.service';
import { HandbookService } from 'src/app/services/handbook/handbook.service';
import { CertificationInformationGetDto } from 'src/models/certification-information/CertificationInformationGetDto';
import { HandbookGetDto } from 'src/models/handbooks/HandbookGetDto';

@Component({
  selector: 'app-certifications',
  templateUrl: './certifications.page.html',
  styleUrls: ['./certifications.page.scss'],
})
export class CertificationsPage implements OnInit {
  public handbooks: HandbookGetDto[] = [];
  public certificationInformation: CertificationInformationGetDto[] = [];
  public loading: boolean = false;
  public error: boolean = false;
  public errorBtns: string[] = ['Intentar de nuevo'];

  constructor(
    private platform: Platform,
    private handbookService: HandbookService,
    private certificationInformationService: CertificationInformationService,
  ) { }

  ngOnInit() {
    this.setHandbooks();
    this.setCertificationInformation();
  }

  public downloadHandbook(handbookId: number): void {
    try {
      this.setLoading(true);
      this.handbookService.getHandbook(handbookId).subscribe({
        next: async (blob) => {
          if (this.platform.is('capacitor')) {
            const fileName = this.getHandbookName(handbookId).replace(/\s+/g, '-') + '.pdf';
            const base64Data = await this.handbookService.convertBlobToBase64(blob);
    
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

  private setHandbooks(): void {
    this.setLoading(true);
    this.handbookService.getHandbookList().subscribe({
      next: (res) => {
        this.handbooks = res.reverse();
        this.setLoading(false);
      },
      error: (error) => {
        this.setError(true);
        this.setLoading(false);
      },
      complete: () => {
        this.setLoading(false);
      }
    })
  }

  private setCertificationInformation(): void {
    this.setLoading(true);
    this.certificationInformationService.getCertificationInformation().subscribe({
      next: (res) => {
        this.certificationInformation = res;
        this.setLoading(false);
      },
      error: (error) => {
        this.setLoading(false);
        this.setError(true);
      },
      complete: () => {
        this.setLoading(false);
      },
    });
  }

  private getHandbookName(handbookId: number): string {
    let [ _handbook ] = this.handbooks.filter(h => h.handbookId === handbookId);
    if (_handbook !== null) return _handbook.name;
    return  '';
  }

  private setLoading(flg: boolean) {
    this.loading = flg;
  }

  public setError(flg: boolean) {
    if (!flg) this.setHandbooks();
    this.error = flg;
  }

}
