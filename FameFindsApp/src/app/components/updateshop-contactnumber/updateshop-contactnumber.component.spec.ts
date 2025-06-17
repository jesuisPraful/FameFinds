import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateShopContactnumberComponent } from './updateshop-contactnumber.component';

describe('UpdateshopContactnumberComponent', () => {
  let component: UpdateShopContactnumberComponent;
  let fixture: ComponentFixture<UpdateShopContactnumberComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateShopContactnumberComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateShopContactnumberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
