import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IShop } from '../../Models/shop';
import { ShopService } from '../../services/shop.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-shop-details',
  templateUrl: './shop-details.component.html',
  styleUrls: ['./shop-details.component.css']
})
export class ShopDetailsComponent implements OnInit {
  shop: IShop | null = null;
  Math = Math;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private shopService: ShopService,
    private location: Location
  ) { }
  goBackin() {
    this.location.back();
  }

  logout() {
    this.router.navigate(['/login']);
  }

  ngOnInit(): void {
    const storedShopId = localStorage.getItem('shopId');

    if (storedShopId) {
      const shopId = parseInt(storedShopId, 10);
      this.shopService.getShopById(shopId).subscribe({
        next: (data) => {
          this.shop = data;
        },
        error: (err) => {
          console.error('Error loading shop:', err);
        }
      });
    } else {
      console.warn('No shop ID found in localStorage.');
      this.router.navigate(['/view-shops']);
    }
  }

  goBack() {
    this.router.navigate(['/view-shops']);
  }

  giveRating() {
    const customerId = localStorage.getItem('customerId'); // should be set on login

    if (!this.shop || this.shop.shopId === undefined || this.shop.shopId === null) {
      alert('Shop details not available yet.');
      return;
    }

    if (!customerId) {
      alert('Customer not logged in.');
      return;
    }
    //localStorage.setItem('selectedShopId', this.shop.shopId.toString());
    //localStorage.setItem('customerId', customerId); // optional if already stored

    this.router.navigate(['/rate-shop']);
  }
}
