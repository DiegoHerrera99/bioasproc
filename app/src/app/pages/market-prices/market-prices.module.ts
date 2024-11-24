import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { MarketPricesPageRoutingModule } from './market-prices-routing.module';

import { MarketPricesPage } from './market-prices.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    MarketPricesPageRoutingModule
  ],
  declarations: [MarketPricesPage]
})
export class MarketPricesPageModule {}
