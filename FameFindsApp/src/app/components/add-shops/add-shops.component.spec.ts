import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddShopsComponent } from './add-shops.component';

describe('AddShopsComponent', () => {
  let component: AddShopsComponent;
  let fixture: ComponentFixture<AddShopsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddShopsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddShopsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
