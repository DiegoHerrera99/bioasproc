import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { Browser } from '@capacitor/browser';
import { NewsService } from 'src/app/services/news/news.service';
import { NewsGetDto } from 'src/models/news/NewsGetDto';

@Component({
  selector: 'app-news',
  templateUrl: './news.page.html',
  styleUrls: ['./news.page.scss'],
})
export class NewsPage implements OnInit {
  public news: NewsGetDto[] = [];
  public showMore: boolean[] = [];
  private wordLimit: number = 30; // Limite de palabras a mostrar inicialmente
  public loading: boolean = false;
  public error: boolean = false;
  public errorBtns: string[] = ['Intentar de nuevo'];
  public sizeLimit: number = 3;
  private counter: number = this.sizeLimit;
  public searchResult: NewsGetDto[] = [];
  public limitedSearchResult: NewsGetDto[] = [];

  constructor(
    private newsService: NewsService,
    private sanitizer: DomSanitizer
  ) { }

  ngOnInit() {
    this.setNews();
  }

  private setNews(): void {
    this.setLoading(true);
    this.newsService.getNews().subscribe({
      next: (res) => {
        this.news = res.map(n => {
          //n.body = this.sanitizer.bypassSecurityTrustHtml(String(n.body));
          //if (n.url) n.url = this.sanitizer.bypassSecurityTrustUrl(String(n.url));
          n.imgPath = `https://${n.imgPath}`;
          return n;
        });

        this.searchResult = this.news;
        this.limitedSearchResult = this.news.slice(0, this.counter);

        this.setLoading(false);
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

  public limitText(text: string, limit: number): string {
    const textWithoutHtml = text.replace(/<[^>]*>/g, ' ').trim();
    const words = textWithoutHtml.split(' ');

    if (words.length <= limit) {
      return text; // Devuelve el texto completo si es más corto que el límite
    }

    // Corta el texto y agrega '...'
    return words.slice(0, limit).join(' ') + '...';
  }

  public toggleShowMore(index: number) {
    this.showMore[index] = !this.showMore[index];
  }

  public getSanitizedBody(index: number): SafeHtml {
    return this.sanitizer.bypassSecurityTrustHtml(this.news[index].body);
  }

  public getDisplayText(index: number): SafeHtml {
    const textToDisplay = this.showMore[index] ? this.news[index].body : this.limitText(this.news[index].body, this.wordLimit);
    return this.sanitizer.bypassSecurityTrustHtml(textToDisplay); // Sanitiza el texto mostrado
  }

  public async openInBrowser(url: string): Promise<void> {
    await Browser.open({ url });
  }

  private setLoading(flg: boolean) {
    this.loading = flg;
  }

  public setError(flg: boolean) {
    if (!flg) this.setNews();
    this.error = flg;
  }

  public handleSearchInput(event: any) {
    const query = event.target.value.toLowerCase();
   
    this.searchResult = this.news.filter((n) =>
      n.title.toLowerCase().includes(query) ||
      n.body.toLowerCase().includes(query)
    );

    this.counter = this.sizeLimit;
    this.limitedSearchResult = this.searchResult.slice(0, this.counter);
  }

  public showMoreNews(): void {
    this.counter++;
    this.limitedSearchResult = this.searchResult.slice(0, this.counter);
  }
}
