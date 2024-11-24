import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BottomNavBarComponent } from './bottom-nav-bar/bottom-nav-bar.component';
import { IonicModule } from '@ionic/angular';
import { WelcomeScreenComponent } from './welcome-screen/welcome-screen.component';
import { VideoPlayerComponent } from './video-player/video-player.component';
import { FormsModule } from '@angular/forms';
import { ContactFormComponent } from './contact-form/contact-form.component';

@NgModule({
  declarations: [
    BottomNavBarComponent,
    WelcomeScreenComponent,
    VideoPlayerComponent,
    ContactFormComponent
  ],
  imports: [
    CommonModule,
    IonicModule.forRoot(),
    FormsModule
  ],
  exports: [
    BottomNavBarComponent,
    WelcomeScreenComponent,
    ContactFormComponent
  ]
})

export class ComponentsModule { }