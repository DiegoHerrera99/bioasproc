import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpService } from '../http/http.service';
import { CourseGetDto } from 'src/models/courses/CourseGetDto';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  constructor(
    private httpService: HttpService
  ) { }

  public getCoursesList(): Observable<CourseGetDto[]> {
    return this.httpService.get<CourseGetDto[]>("/Course");
  }
}
