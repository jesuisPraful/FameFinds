import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ShopDetailsComponent } from './shop-details.component';
import { Router } from '@angular/router';
import { IShop } from '../../Models/shop';

describe('ShopDetailsComponent', () => {
  let component: ShopDetailsComponent;
  let fixture: ComponentFixture<ShopDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ShopDetailsComponent],
      providers: [
        { provide: Router, useValue: { navigate: jasmine.createSpy('navigate') } }
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ShopDetailsComponent);
    component = fixture.componentInstance;

    // ✅ Mock shop data to avoid undefined errors
    component.shop = {
      shopName: 'Test Shop',
      emailId: 'test@example.com',
      contactNumber: '1234567890',
      fullAddress: '123 Street, Test City',
      pincode: '400001',
      isOpen: true,
      latitude: 18.5204,
      longitude: 73.8567,
      // If averageRating is used in template:
      averageRating: 4.2
    } as unknown as IShop;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
