import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CertificationsPage } from './certifications.page';

describe('CertificationsPage', () => {
  let component: CertificationsPage;
  let fixture: ComponentFixture<CertificationsPage>;

  beforeEach(() => {
    fixture = TestBed.createComponent(CertificationsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
