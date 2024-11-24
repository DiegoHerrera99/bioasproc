import { Component, OnInit } from '@angular/core';
import { Browser } from '@capacitor/browser';
import { ProductsService } from 'src/app/services/products/products.service';
import { Location } from 'src/models/location/Location';
import { ProductGetDto } from 'src/models/products/ProductGetDto';

@Component({
  selector: 'app-products',
  templateUrl: './products.page.html',
  styleUrls: ['./products.page.scss'],
})
export class ProductsPage implements OnInit {
  private products: ProductGetDto[] = [];
  public sizeLimit: number = 3;
  private counter: number = this.sizeLimit;
  public searchResult: ProductGetDto[] = [];
  public limitedSearchResult: ProductGetDto[] = [];
  public loading: boolean = false;
  public error: boolean = false;
  public emailSuccess: boolean = false;
  public errorBtns: string[] = ['Intentar de nuevo'];
  public successBtns: string[] = ['Enterado'];
  private location: Location = {
    latitude: 14.741910851913666,
    longitude: -90.90387281087942
  };
  public googleMapsUrl: string = `https://www.google.com/maps/dir/?api=1&destination=${this.location.latitude},${this.location.longitude}`;
  public wazeUrl: string = `https://waze.com/ul?ll=${this.location.latitude},${this.location.longitude}&navigate=yes`;

  constructor(
    private productsService: ProductsService
  ) { }

  ngOnInit() {
    this.setProducts();
  }

  private setProducts(): void {
    this.setLoading(true);
    this.productsService.getProducts().subscribe({
      next: (res) => {
        this.products = res.map(p => {
          p.imgPath = `https://${p.imgPath}`;

          return p;
        });
        this.searchResult = this.products;
        this.limitedSearchResult = this.products.slice(0, this.counter);
        this.setLoading(false);
      },
      error: (error) => {
        this.setLoading(false);
        this.setError(true);
      },
      complete: () => {
        this.setLoading(false);
      },
    });
  }

  public handleSearchInput(event: any) {
    const query = event.target.value.toLowerCase();
   
    this.searchResult = this.products.filter((n) =>
      n.title.toLowerCase().includes(query) ||
      n.description.toLowerCase().includes(query)
    );

    this.counter = this.sizeLimit;
    this.limitedSearchResult = this.searchResult.slice(0, this.counter);
  }

  public showMoreNews(): void {
    this.counter++;
    this.limitedSearchResult = this.searchResult.slice(0, this.counter);
  }

  public setLoading(flg: boolean) {
    this.loading = flg;
  }

  public setError(flg: boolean) {
    if (!flg) this.setProducts();
    this.error = flg;
  }

  public setEmailSuccess(flg: boolean) {
    if (!flg) this.setProducts();
    this.emailSuccess = flg;
  }

  public async openInBrowser(url: string): Promise<void> {
    await Browser.open({ url });
  }
}
