import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewShopsComponent } from './view-shops.component';

describe('ViewShopsComponent', () => {
  let component: ViewShopsComponent;
  let fixture: ComponentFixture<ViewShopsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewShopsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewShopsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
