import { Component, OnInit } from '@angular/core';
import { IShop } from '../../Models/shop';
import { ShopService } from '../../services/shop.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shop-details',
  templateUrl: './shop-details.component.html',
  styleUrls: ['./shop-details.component.css']
})
export class ShopDetailsComponent implements OnInit {
  shop!: IShop & { averageRating?: number };

  constructor(private _service: ShopService, private _router: Router) { }

  ngOnInit(): void {
    const storedShopId = localStorage.getItem('shopId');
    const shopData = history.state.shop;

    if (shopData && shopData.shopId) {
      this.shop = { ...shopData, averageRating: 0 };

      // Fetch average rating from backend
      this._service.getAverageRating(this.shop.shopId).subscribe({
        next: (avg: number) => {
          this.shop.averageRating = avg;
        },
        error: (err) => {
          console.error(`Failed to fetch average rating for shop ${this.shop.shopId}`, err);
        }
      });

    } else {
      console.warn('No shop data passed. Redirecting...');
      this._router.navigate(['/view-shops']);
    }
  }

  goBack(): void {
    this._router.navigate(['/view-shops']);
  }

  giveRating(): void {
    if (this.shop && this.shop.shopId) {
      localStorage.setItem('shopId', this.shop.shopId.toString());
      this._router.navigate(['/rate-shop']);
    }
  }

    if (!customerId) {
      alert('Customer not logged in.');
      return;
    }
    //localStorage.setItem('selectedShopId', this.shop.shopId.toString());
    //localStorage.setItem('customerId', customerId); // optional if already stored

    this.router.navigate(['/rate-shop']);
  }
  openInMap(): void {
    const url = `https://www.google.com/maps/search/?api=1&query=${this.shop.latitude},${this.shop.longitude}`;
    window.open(url, '_blank');
  }

  Math = Math; // For template usage
}
