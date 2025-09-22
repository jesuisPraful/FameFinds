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
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
