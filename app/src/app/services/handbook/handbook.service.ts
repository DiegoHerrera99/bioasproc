import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../http/http.service';
import { HandbookGetDto } from 'src/models/handbooks/HandbookGetDto';

@Injectable({
  providedIn: 'root'
})
export class HandbookService {
  constructor(
    private httpService: HttpService
  ) { }

  public getHandbookList(): Observable<HandbookGetDto[]> {
    return this.httpService.get<HandbookGetDto[]>("/Handbook");
  }


  public getHandbook(handbookId: number): Observable<Blob> {
    return this.httpService.get<Blob>("/Handbook/" + handbookId, null, undefined, 'blob');
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


