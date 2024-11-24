import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Observable } from 'rxjs';
import { WeatherAlertGetDto } from 'src/models/weather-alerts/WeatherAlertGetDto';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  constructor(
    private http: HttpService
  ) { }

  public getWeatherAlerts(): Observable<WeatherAlertGetDto[]> {
    return this.http.get<WeatherAlertGetDto[]>('/WheaterAlert');
  }
}
