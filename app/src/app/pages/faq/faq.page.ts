import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Browser } from '@capacitor/browser';
import { FaqService } from 'src/app/services/faq/faq.service';
import { MailService } from 'src/app/services/mail/mail.service';
import { FaqGetDto } from 'src/models/faq/FaqGetDto';
import { Location } from 'src/models/location/Location';
import { SendEmailRequestDto } from 'src/models/mail/SendEmailRequestDto';

@Component({
  selector: 'app-faq',
  templateUrl: './faq.page.html',
  styleUrls: ['./faq.page.scss'],
})
export class FaqPage implements OnInit {
  public faq: FaqGetDto[] = [];
  public sizeLimit: number = 3;
  private counter: number = this.sizeLimit;
  public searchResult: FaqGetDto[] = [];
  public limitedSearchResult: FaqGetDto[] = [];
  public loading: boolean = false;
  public error: boolean = false;
  public emailSuccess: boolean = false;
  public errorBtns: string[] = ['Intentar de nuevo'];
  public successBtns: string[] = ['Enterado'];
  private location: Location = {
    latitude: 14.741910851913666,
    longitude: -90.90387281087942
  };
  public googleMapsUrl: string = `https://www.google.com/maps/dir/?api=1&destination=${this.location.latitude},${this.location.longitude}`;
  public wazeUrl: string = `https://waze.com/ul?ll=${this.location.latitude},${this.location.longitude}&navigate=yes`;

  constructor(
    private faqService: FaqService,
    private mailService: MailService
  ) {}

  ngOnInit(): void {
    this.setFaq();
  }

  private setFaq(): void {
    this.setLoading(true);
    this.faqService.getFaq().subscribe({
      next: (res) => {
        this.faq = res;
        this.searchResult = res;
        this.limitedSearchResult = res.slice(0, this.counter);
        this.setLoading(false);
      },
      error: (error) => {
        this.setLoading(false);
        this.setError(true);
      },
      complete: () => {
        this.setLoading(false);
      }
    });
  }

  public handleSearchInput(event: any) {
    const query = event.target.value.toLowerCase();
   
    this.searchResult = this.faq.filter((f) =>
      f.question.toLowerCase().includes(query) ||
      f.answer.toLowerCase().includes(query)
    );

    this.counter = this.sizeLimit;
    this.limitedSearchResult = this.searchResult.slice(0, this.counter);
  }

  public setLoading(flg: boolean) {
    this.loading = flg;
  }

  public setError(flg: boolean) {
    if (!flg) this.setFaq();
    this.error = flg;
  }

  public setEmailSuccess(flg: boolean) {
    if (!flg) this.setFaq();
    this.emailSuccess = flg;
  }

  public showMore(): void {
      this.counter++;
      this.limitedSearchResult = this.searchResult.slice(0, this.counter);
  }

  public onSubmit(f: NgForm): void {
    this.setLoading(true);
    let { name, phone, email, question } = f.value;
    let params: SendEmailRequestDto = { name, phone, email, question };
    this.mailService.sendEmail(params).subscribe({
      next: (res) => {
        this.setLoading(false);
        this.setEmailSuccess(true);
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

  public async openInBrowser(url: string): Promise<void> {
    await Browser.open({ url });
  }
}
