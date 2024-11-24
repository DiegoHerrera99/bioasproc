import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Observable } from 'rxjs';
import { ProductGetDto } from 'src/models/products/ProductGetDto';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(
    private http: HttpService
  ) { }

  public getProducts(): Observable<ProductGetDto[]> {
    return this.http.get<ProductGetDto[]>('/Product');
  }
}
