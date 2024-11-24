import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MarketPricesPage } from './market-prices.page';

describe('MarketPricesPage', () => {
  let component: MarketPricesPage;
  let fixture: ComponentFixture<MarketPricesPage>;

  beforeEach(() => {
    fixture = TestBed.createComponent(MarketPricesPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
