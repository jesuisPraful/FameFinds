import { Component, OnInit } from '@angular/core';
import { IShop } from '../../Models/shop';
import { ShopService } from '../../services/shop.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-shops',
  templateUrl: './view-shops.component.html',
  styleUrls: ['./view-shops.component.css']
})
export class ViewShopsComponent implements OnInit {
  shops: IShop[] = [];
  filteredShops: IShop[] = [];
  productName: string = '';
  cityName: string = '';
  selectedShop: IShop | null = null;
  showMsgDiv: boolean = false;

  constructor(private _service: ShopService, private _router: Router) { }

  ngOnInit(): void {
    const storedProduct = localStorage.getItem('selectedProduct');
    const storedCity = localStorage.getItem('selectedCity');

    if (storedProduct && storedCity) {
      this.productName = storedProduct;
      this.cityName = storedCity;

      this._service.getShopsByProductAndCityNames(this.productName, this.cityName).subscribe({
        next: (data) => {
          this.shops = data;
          this.filteredShops = data;
          this.showMsgDiv = this.filteredShops.length === 0;
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
  

  //showMap(shop: IShop) {
  //  this.selectedShop = shop;

  //  setTimeout(() => {
  //    const map = new google.maps.Map(document.getElementById("map") as HTMLElement, {
  //      center: { lat: shop.latitude, lng: shop.longitude },
  //      zoom: 15
  //    });

  //    new google.maps.Marker({
  //      position: { lat: shop.latitude, lng: shop.longitude },
  //      map,
  //      title: shop.shopName
  //    });
  //  }, 0);
  //}

  viewShopDetails(shop: IShop) {
    this._router.navigate(['/shop-details'], { state: { shop } });
  }

  showMap(shop: IShop) {
    const mapUrl = `https://www.google.com/maps/search/?api=1&query=${shop.latitude},${shop.longitude}`;
    window.open(mapUrl, '_blank');
  }
}
