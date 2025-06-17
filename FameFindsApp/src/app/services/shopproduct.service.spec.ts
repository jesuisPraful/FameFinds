import { TestBed } from '@angular/core/testing';

import { ShopProductService } from './shopproduct.service';

describe('ShopproductService', () => {
  let service: ShopProductService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShopProductService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
