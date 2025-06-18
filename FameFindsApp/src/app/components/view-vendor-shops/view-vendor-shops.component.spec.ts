import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewVendorShopsComponent } from './view-vendor-shops.component';

describe('ViewVendorShopsComponent', () => {
  let component: ViewVendorShopsComponent;
  let fixture: ComponentFixture<ViewVendorShopsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewVendorShopsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewVendorShopsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
