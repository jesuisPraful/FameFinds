import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForgotPasswordVendorComponent } from './forgot-password-vendor.component';

describe('ForgotPasswordVendorComponent', () => {
  let component: ForgotPasswordVendorComponent;
  let fixture: ComponentFixture<ForgotPasswordVendorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ForgotPasswordVendorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ForgotPasswordVendorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
