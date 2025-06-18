import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common'

@Component({
  selector: 'app-vendor-shop-options',
  templateUrl: './vendor-shop-options.component.html',
  styleUrls: ['./vendor-shop-options.component.css']
})
export class VendorShopOptionsComponent {

  constructor(private router: Router, private location: Location
) { }
  goBack() {
    this.location.back();
  }

  logout() {
    this.router.navigate(['/vendorlogin']);
  }

  navigateTo(path: string): void {
    this.router.navigate([`/${path}`]);
  }
}
