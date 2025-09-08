import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

import { VendorShopService } from '../../services/vendor-shop.service';
import { IShop } from '../../Models/shop';

@Component({
  selector: 'app-view-vendor-shops',
  templateUrl: './view-vendor-shops.component.html',
  styleUrls: ['./view-vendor-shops.component.css']
})
export class ViewVendorShopsComponent implements OnInit {
  filteredShops: IShop[] = [];

  // City ID to Name Mapping
  cityMap: { [key: number]: string } = {
    1: 'HYDERABAD',
    2: 'MUMBAI',
    3: 'KOLKATA',
    4: 'BANGALORE',
    5: 'DELHI',
    6: 'CHENNAI'
  };

  constructor(
    private shopService: VendorShopService,
    private router: Router,
    private location: Location
  ) { }

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

  // Return City Name from ID
  getCityName(cityId: number): string {
    return this.cityMap[cityId] || 'Unknown City';
  }

  // Navigate to Add Product Page
  goToAddProduct(shopId: number): void {
    localStorage.setItem('selectedShopId', shopId.toString());
    this.router.navigate(['/add-shop-product']);
  }

  // Navigate to View Products Page
  goToViewProduct(shopId: number): void {
    localStorage.setItem('selectedShopId', shopId.toString());
    this.router.navigate(['/view-shop-products']);
  }

  // Navigate Back
  goBack(): void {
    this.location.back();
  }

  // Logout and redirect to login
  logout(): void {
    this.router.navigate(['/vendorlogin']);
  }
}

