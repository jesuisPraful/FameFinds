import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IShop } from '../../Models/shop';
import { ShopService } from '../../services/shop.service';

@Component({
  selector: 'app-add-shops',
  templateUrl: './add-shops.component.html',
  styleUrls: ['./add-shops.component.css']
})
export class AddShopsComponent {
  newShop: IShop = {
    shopId: 0,
    shopName: '',
    emailId: '',
    cityId: null,
    pincode: '',
    contactNumber: '',
    fullAddress: '',
    latitude: null,
    longitude: null,
    openingTime: new Date(),
    closingTime: new Date(),
    isOpen: false,
    createdAt: new Date(),
    vendorId: 0
  };

  message: string = '';

  constructor(private _shopService: ShopService, private _router: Router) { }

  addShop() {
    this._shopService.addShop(this.newShop).subscribe(
      (response) => {
        console.log('Shop added successfully:', response);
        this.message = 'Shop added successfully!';
        this._router.navigate(['/view-shops']); // redirect if needed
      },
      (error) => {
        console.error('Error adding shop:', error);
        this.message = 'Error adding shop. Please try again.';
      }
    );
  }
}
