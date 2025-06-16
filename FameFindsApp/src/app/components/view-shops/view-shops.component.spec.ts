import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ViewShopsComponent } from './view-shops.component';
import { Router } from '@angular/router';
import { ShopService } from '../../services/shop.service';
import { of } from 'rxjs';

describe('ViewShopsComponent', () => {
  let component: ViewShopsComponent;
  let fixture: ComponentFixture<ViewShopsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ViewShopsComponent],
      providers: [
        {
          provide: ShopService,
          useValue: {
            getShopsByProductAndCityNames: () => of([]) // return empty observable by default
          }
        },
        {
          provide: Router,
          useValue: {
            navigate: jasmine.createSpy('navigate')
          }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ViewShopsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
