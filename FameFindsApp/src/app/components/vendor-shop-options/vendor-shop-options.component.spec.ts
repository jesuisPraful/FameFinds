import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorShopOptionsComponent } from './vendor-shop-options.component';

describe('VendorShopOptionsComponent', () => {
  let component: VendorShopOptionsComponent;
  let fixture: ComponentFixture<VendorShopOptionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VendorShopOptionsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VendorShopOptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
