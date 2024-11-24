import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { CertificationsPageRoutingModule } from './certifications-routing.module';

import { CertificationsPage } from './certifications.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    CertificationsPageRoutingModule
  ],
  declarations: [CertificationsPage]
})
export class CertificationsPageModule {}
