import { Component, OnInit } from '@angular/core';
import { ShopService } from 'src/app/services/shop.service';
import { VendorShopService } from '../../vendor-shop.service';
import { IShop } from '../../Models/shop';
import { Router } from '@angular/router';
import { Location } from '@angular/common'

@Component({
  selector: 'app-view-vendor-shops',
  templateUrl: './view-vendor-shops.component.html',
  styleUrls: ['./view-vendor-shops.component.css']
})
export class ViewVendorShopsComponent implements OnInit {
  filteredShops: IShop[] = [];

  
  constructor(private shopService: VendorShopService, private router: Router, private location: Location) { }
  // Hardcoded cityId → cityName map
  cityMap: { [key: number]: string } = {
    1: 'HYDERABAD',
    2: 'MUMBAI',
    3: 'KOLKATA',
    4: 'BANGALORE',
    5: 'DELHI',
    6: 'CHENNAI'
  };
  
  logout() {
    this.router.navigate(['/vendorlogin']);
  }
  goBack() {
    this.location.back();
  }


  ngOnInit(): void {
    const vendorIdStr = localStorage.getItem('vendorId');
    if (vendorIdStr) {
      const vendorId = Number(vendorIdStr);
      this.shopService.getShopsByVendorId(vendorId).subscribe({
        next: (shops) => {
          console.log('Shops fetched:', shops);
          this.filteredShops = shops;
          
        },
        error: (err) => {
          console.error('Error fetching shops:', err);
        }
      });
    }
  }

  getCityName(cityId: number): string {
    return this.cityMap[cityId] || 'Unknown City';
  }

  goToAddProduct(shopId: number): void {
    localStorage.setItem('selectedShopId', shopId.toString()); // ✅ Save to localStorage
    //this.router.navigate(['/add-shop-product']);
    this.router.navigate(['/vendor-shop-options']);// ✅ Navigate
  }
}
