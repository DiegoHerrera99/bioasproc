import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CertificationsPage } from './certifications.page';

const routes: Routes = [
  {
    path: '',
    component: CertificationsPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CertificationsPageRoutingModule {}
