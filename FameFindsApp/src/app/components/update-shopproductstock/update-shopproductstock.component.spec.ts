import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateShopproductstockComponent } from './update-shopproductstock.component';

describe('UpdateShopproductstockComponent', () => {
  let component: UpdateShopproductstockComponent;
  let fixture: ComponentFixture<UpdateShopproductstockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateShopproductstockComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateShopproductstockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
