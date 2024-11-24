import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Observable } from 'rxjs';
import { VideoGetDto } from 'src/models/videos/VideoGetDto';

@Injectable({
  providedIn: 'root'
})
export class VideosService {

  constructor(
    private httpService: HttpService
  ) { }

  public getStream(courseId: number): Observable<VideoGetDto> {
    return this.httpService.get<VideoGetDto>(`/Video/stream/${courseId}`);
  }
}
