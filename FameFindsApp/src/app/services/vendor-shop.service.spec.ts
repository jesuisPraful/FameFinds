import { TestBed } from '@angular/core/testing';

import { VendorShopService } from './vendor-shop.service';

describe('VendorShopService', () => {
  let service: VendorShopService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VendorShopService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
