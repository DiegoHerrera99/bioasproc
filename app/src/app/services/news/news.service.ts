import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Observable } from 'rxjs';
import { NewsGetDto } from 'src/models/news/NewsGetDto';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  constructor(
    private http: HttpService
  ) { }

  public getNews(): Observable<NewsGetDto[]> {
    return this.http.get<NewsGetDto[]>('/News');
  }
}
