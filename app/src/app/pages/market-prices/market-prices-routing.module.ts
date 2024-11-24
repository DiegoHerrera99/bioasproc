import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MarketPricesPage } from './market-prices.page';

const routes: Routes = [
  {
    path: '',
    component: MarketPricesPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MarketPricesPageRoutingModule {}
