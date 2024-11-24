import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FaqGetDto } from 'src/models/faq/FaqGetDto';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class FaqService {

  constructor(
    private http: HttpService
  ) { }

  public getFaq(): Observable<FaqGetDto[]> {
    return this.http.get<FaqGetDto[]>('/Faq');
  }
}
