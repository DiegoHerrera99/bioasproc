import { Component, OnInit } from '@angular/core';
import { PricesService } from 'src/app/services/prices/prices.service';
import { PriceGetDto } from 'src/models/prices/PriceGetDto';

@Component({
  selector: 'app-market-prices',
  templateUrl: './market-prices.page.html',
  styleUrls: ['./market-prices.page.scss'],
})
export class MarketPricesPage implements OnInit {
  private prices: PriceGetDto[] = []; 
  public sizeLimit: number = 3;
  private counter: number = this.sizeLimit;
  public searchResult: PriceGetDto[] = [];
  public limitedSearchResult: PriceGetDto[] = [];
  public loading: boolean = false;
  public error: boolean = false;
  public errorBtns: string[] = ['Intentar de nuevo'];

  constructor(
    private priceService: PricesService,
  ) { }

  ngOnInit() {
    this.setPrices();
  }

  private setPrices(): void {
    this.setLoading(true);
    this.priceService.getPrices().subscribe({
      next: (res) => {
        this.prices = res.map(p => {
          p.imgPath = `https://${p.imgPath}`;

          return p;
        });
        this.searchResult = this.prices;
        this.limitedSearchResult = this.prices.slice(0, this.counter);
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
   
    this.searchResult = this.prices.filter((n) =>
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
    if (!flg) this.setPrices();
    this.error = flg;
  }
}
