import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class PdfsService {
  constructor(
    private httpService: HttpService
  ) { }

  public getPdf(courseId: number): Observable<Blob> {
    return this.httpService.get<Blob>("/Pdf/" + courseId, null, undefined, 'blob');
  }

  public convertBlobToBase64(blob: Blob): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onerror = reject;
      reader.onload = () => {
        const dataUrl = reader.result as string;
        resolve(dataUrl.split(',')[1]);
      };
      reader.readAsDataURL(blob);
    });
  }
}

