import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateShopproductpriceComponent } from './update-shopproductprice.component';

describe('UpdateShopproductpriceComponent', () => {
  let component: UpdateShopproductpriceComponent;
  let fixture: ComponentFixture<UpdateShopproductpriceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateShopproductpriceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateShopproductpriceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
