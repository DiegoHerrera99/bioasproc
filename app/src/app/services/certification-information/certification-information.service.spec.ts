import { TestBed } from '@angular/core/testing';

import { CertificationInformationService } from './certification-information.service';

describe('CertificationInformationService', () => {
  let service: CertificationInformationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CertificationInformationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
