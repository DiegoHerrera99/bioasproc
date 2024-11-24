import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-welcome-screen',
  templateUrl: './welcome-screen.component.html',
  styleUrls: ['./welcome-screen.component.scss'],
})
export class WelcomeScreenComponent  implements OnInit {

  @Output() clickEvent: EventEmitter<boolean> = new EventEmitter(false);

  constructor() { }

  ngOnInit() {}

  public triggerClickEvent(): void {
    this.clickEvent.emit(true);
  }
}
