import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowShopproductComponent } from './show-shopproduct.component';

describe('ShowShopproductComponent', () => {
  let component: ShowShopproductComponent;
  let fixture: ComponentFixture<ShowShopproductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ShowShopproductComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowShopproductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
