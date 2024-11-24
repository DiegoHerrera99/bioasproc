import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { PriceGetDto } from 'src/models/prices/PriceGetDto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PricesService {

  constructor(
    private http: HttpService
  ) { }

  public getPrices(): Observable<PriceGetDto[]> {
    return this.http.get<PriceGetDto[]>('/Price');
  }
}
