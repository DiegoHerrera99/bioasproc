import { Component, OnChanges, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  public showWelcomeScreen: boolean = true;
  
  constructor(
    private router: Router
  ) {}

  public setShowWelcomeScreen(flg: boolean) {
    this.showWelcomeScreen = flg;
  }
}
