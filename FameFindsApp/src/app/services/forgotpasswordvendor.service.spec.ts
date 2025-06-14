import { TestBed } from '@angular/core/testing';

import { ForgotpasswordvendorService } from './forgotpasswordvendor.service';

describe('ForgotpasswordvendorService', () => {
  let service: ForgotpasswordvendorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ForgotpasswordvendorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
