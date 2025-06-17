import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateshopEmailComponent } from './updateshop-email.component';

describe('UpdateshopEmailComponent', () => {
  let component: UpdateshopEmailComponent;
  let fixture: ComponentFixture<UpdateshopEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateshopEmailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateshopEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
