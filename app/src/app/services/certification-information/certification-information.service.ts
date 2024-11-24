import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { CertificationInformationGetDto } from 'src/models/certification-information/CertificationInformationGetDto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CertificationInformationService {

  constructor(
    private httpService: HttpService
  ) { }

  public getCertificationInformation(): Observable<CertificationInformationGetDto[]> {
    return this.httpService.get<CertificationInformationGetDto[]>("/CertificationInformation");
  }
}
