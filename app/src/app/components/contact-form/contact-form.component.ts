import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MailService } from 'src/app/services/mail/mail.service';
import { SendEmailRequestDto } from 'src/models/mail/SendEmailRequestDto';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.scss'],
})
export class ContactFormComponent  implements OnInit {
  @Output() onLoading = new EventEmitter<boolean>(false);
  @Output() onError = new EventEmitter<boolean>(false);
  @Output() onSuccess = new EventEmitter<boolean>(false);

  constructor(
    private mailService: MailService
  ) { }

  ngOnInit() {}

  public onSubmit(f: NgForm): void {
    this.onLoading.emit(true);
    let { name, phone, email, question } = f.value;
    let params: SendEmailRequestDto = { name, phone, email, question };
    this.mailService.sendEmail(params).subscribe({
      next: (res) => {
        this.onLoading.emit(false);
        this.onSuccess.emit(true);
      },
      error: (error) => {
        this.onLoading.emit(false);
        this.onError.emit(true);
      },
      complete: () => {
        this.onLoading.emit(false);
      }, 
    });
  }
}
