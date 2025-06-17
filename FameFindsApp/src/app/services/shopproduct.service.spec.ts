import { TestBed } from '@angular/core/testing';

import { ShopproductService } from './shopproduct.service';

describe('ShopproductService', () => {
  let service: ShopproductService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShopproductService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
