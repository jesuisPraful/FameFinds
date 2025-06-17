import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateshopNameComponent } from './updateshop-name.component';

describe('UpdateshopNameComponent', () => {
  let component: UpdateshopNameComponent;
  let fixture: ComponentFixture<UpdateshopNameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateshopNameComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateshopNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
