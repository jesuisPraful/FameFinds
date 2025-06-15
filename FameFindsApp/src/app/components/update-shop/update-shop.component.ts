//import { Component } from '@angular/core';
//import { ShopService } from '../../services/shop.service';

//@Component({
//  selector: 'app-update-shop',
//  templateUrl: './update-shop.component.html',
//  styleUrls: ['./update-shop.component.css']
//})
//export class UpdateShopComponent {
//  shopId!: number;
//  contactNumber: string = '';
//  emailId: string = '';

//  constructor(private shopService: ShopService) { }

//  updateShop(): void {
//    if (!this.shopId) {
//      console.error('Shop ID is required');
//      return;
//    }

//    // Update contact number if provided
//    if (this.contactNumber.trim()) {
//      this.shopService.updateShopContactNumber(this.shopId, this.contactNumber.trim()).subscribe({
//        next: () => console.log('Contact number updated'),
//        error: err => console.error('Contact number update failed:', err)
//      });
//    }

//    // Update email if provided
//    if (this.emailId.trim()) {
//      this.shopService.updateShopEmail(this.shopId, this.emailId.trim()).subscribe({
//        next: () => console.log('Email updated'),
//        error: err => console.error('Email update failed:', err)
//      });
//    }
//  }
//}
