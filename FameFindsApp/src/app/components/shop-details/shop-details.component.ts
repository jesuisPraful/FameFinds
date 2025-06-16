import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IShop } from '../../Models/shop';

@Component({
  selector: 'app-shop-details',
  templateUrl: './shop-details.component.html',
  styleUrls: ['./shop-details.component.css']
})
export class ShopDetailsComponent implements OnInit {
  shop!: IShop & { averageRating?: number };

  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    const shopData = history.state.shop;
    if (shopData) {
      this.shop = {
        ...shopData,
        averageRating: shopData.averageRating ?? 0 // fallback for missing value
      };
    } else {
      this.router.navigate(['/view-shops']); // Fallback if data is not passed
    }
  }

  goBack() {
    this.router.navigate(['/view-shops']);
  }

  openInMap() {
    const url = `https://www.google.com/maps/search/?api=1&query=${this.shop.latitude},${this.shop.longitude}`;
    window.open(url, '_blank');
  }

  giveRating() {
    this.router.navigate(['/rate-shop'], { state: { shopId: this.shop.shopId } });
  }

  rateShop() {
    this.router.navigate(['/rate-shop'], { state: { shopId: this.shop.shopId } });
  }

  Math = Math; // Needed for using Math in template
}


