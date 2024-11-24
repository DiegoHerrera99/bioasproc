import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private gateway: string = environment.URL_GATEWAY;

  constructor(private http: HttpClient) { }

  public get<T>(
    endpoint: string, 
    params?: any, 
    headers?: HttpHeaders, 
    responseType: 'json' | 'blob' | 'text' = 'json'
  ): Observable<T> {
    const options = {
      params: new HttpParams({ fromObject: params }),
      headers: headers,
      responseType: responseType as 'json'
    };
    return this.http.get<T>(this.gateway + endpoint, options);
  }

  public post<T>(
    endpoint: string, 
    body: any, 
    headers?: HttpHeaders, 
    responseType: 'json' | 'blob' | 'text' = 'json'
  ): Observable<T> {
    const options = {
      headers: headers,
      responseType: responseType as 'json'
    };
    return this.http.post<T>(this.gateway + endpoint, body, options);
  }

  public put<T>(
    endpoint: string, 
    body: any, 
    headers?: HttpHeaders, 
    responseType: 'json' | 'blob' | 'text' = 'json'
  ): Observable<T> {
    const options = {
      headers: headers,
      responseType: responseType as 'json'
    };
    return this.http.put<T>(this.gateway + endpoint, body, options);
  }

  public delete<T>(
    endpoint: string, 
    params?: any, 
    headers?: HttpHeaders, 
    responseType: 'json' | 'blob' | 'text' = 'json'
  ): Observable<T> {
    const options = {
      params: new HttpParams({ fromObject: params }),
      headers: headers,
      responseType: responseType as 'json'
    };
    return this.http.delete<T>(this.gateway + endpoint, options);
  }
}
