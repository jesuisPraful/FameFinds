import { TestBed } from '@angular/core/testing';

import { Register2Service } from './register2.service';

describe('Register2Service', () => {
  let service: Register2Service;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Register2Service);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
