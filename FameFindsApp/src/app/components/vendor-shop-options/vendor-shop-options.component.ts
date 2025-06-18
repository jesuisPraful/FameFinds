import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vendor-shop-options',
  templateUrl: './vendor-shop-options.component.html',
  styleUrls: ['./vendor-shop-options.component.css']
})
export class VendorShopOptionsComponent {

  constructor(private router: Router) { }

  navigateTo(path: string): void {
    this.router.navigate([`/${path}`]);
  }
}
