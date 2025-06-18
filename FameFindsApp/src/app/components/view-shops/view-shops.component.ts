import { Component, OnInit } from '@angular/core';
import { IShop } from '../../Models/shop';
import { ShopService } from '../../services/shop.service';
import { Router } from '@angular/router';
import { RatingService } from '../../services/rating.service';
import { Location } from '@angular/common'

@Component({
  selector: 'app-view-shops',
  templateUrl: './view-shops.component.html',
  styleUrls: ['./view-shops.component.css']
})
export class ViewShopsComponent implements OnInit {
  shops: (IShop & { averageRating?: number })[] = [];
  filteredShops: (IShop & { averageRating?: number })[] = [];
  productName: string = '';
  cityName: string = '';
  selectedShop: IShop | null = null;
  showMsgDiv: boolean = false;

  constructor(private _service: ShopService, private _router: Router, private _ratingService: RatingService, private location: Location
) { }


  goBack() {
    this.location.back();
  }

  logout() {
    this._router.navigate(['/login']);
  }

  ngOnInit(): void {
    const storedProduct = localStorage.getItem('selectedProduct');
    const storedCity = localStorage.getItem('selectedCity');

    if (storedProduct && storedCity) {
      this.productName = storedProduct;
      this.cityName = storedCity;

      this._service.getShopsByProductAndCityNames(this.productName, this.cityName).subscribe({
        next: (data) => {
          this.shops = data.map(shop => ({
            ...shop,
            averageRating: 0
          }));

          this.filteredShops = this.shops;
          this.showMsgDiv = this.filteredShops.length === 0;

          // Fetch average ratings for each shop
          this.filteredShops.forEach(shop => {
            this._ratingService.getAverageRating(shop.shopId).subscribe({
              next: avg => {
                shop.averageRating = avg.averageRating;
                shop.totalRating = avg.totalRatings; // optional
              },

              error: err => console.error(`Failed to load rating for shop ${shop.shopId}`, err)
            });
          });
        },
        error: (err) => {
          console.error('Error fetching shops:', err);
          this.shops = [];
          this.filteredShops = [];
          this.showMsgDiv = true;
        }
      });
    } else {
      console.warn('Product name or city name not found in localStorage.');
      this.showMsgDiv = true;
    }
  }

  viewShopDetails(shop: IShop) {
    localStorage.setItem('shopId', shop.shopId.toString());
    this._router.navigate(['/shop-details'], { state: { shop } });
  }

  showMap(shop: IShop) {
    const mapUrl = `https://www.google.com/maps/search/?api=1&query=${shop.latitude},${shop.longitude}`;
    window.open(mapUrl, '_blank');
  }

  Math = Math;
}
