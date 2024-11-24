import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Observable } from 'rxjs';
import { SendEmailRequestDto } from 'src/models/mail/SendEmailRequestDto';
import { SendEmailResponseDto } from 'src/models/mail/SendEmailResponse';

@Injectable({
  providedIn: 'root'
})
export class MailService {

  constructor(
    private http: HttpService
  ) { }

  public sendEmail(request: SendEmailRequestDto): Observable<SendEmailResponseDto> {
    return this.http.post<SendEmailResponseDto>('/Email', request);
  }
}
