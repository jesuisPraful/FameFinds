import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateShopproductComponent } from './update-shopproduct.component';

describe('UpdateShopproductComponent', () => {
  let component: UpdateShopproductComponent;
  let fixture: ComponentFixture<UpdateShopproductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateShopproductComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateShopproductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
