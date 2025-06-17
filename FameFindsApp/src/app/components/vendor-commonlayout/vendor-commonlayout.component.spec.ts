import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorCommonlayoutComponent } from './vendor-commonlayout.component';

describe('VendorCommonlayoutComponent', () => {
  let component: VendorCommonlayoutComponent;
  let fixture: ComponentFixture<VendorCommonlayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VendorCommonlayoutComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VendorCommonlayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
