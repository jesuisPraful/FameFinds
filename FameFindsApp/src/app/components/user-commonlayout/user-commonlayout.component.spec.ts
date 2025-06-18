import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserCommonlayoutComponent } from './user-commonlayout.component';

describe('UserCommonlayoutComponent', () => {
  let component: UserCommonlayoutComponent;
  let fixture: ComponentFixture<UserCommonlayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserCommonlayoutComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserCommonlayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
