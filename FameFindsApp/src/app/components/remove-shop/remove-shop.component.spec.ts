import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveShopComponent } from './remove-shop.component';

describe('RemoveShopComponent', () => {
  let component: RemoveShopComponent;
  let fixture: ComponentFixture<RemoveShopComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemoveShopComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RemoveShopComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
