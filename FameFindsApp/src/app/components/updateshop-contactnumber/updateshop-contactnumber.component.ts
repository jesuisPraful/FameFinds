import { Component } from '@angular/core';
import { Router } from '@angular/router'; // <-- Added
import { ShopService } from '../../services/shop.service';

@Component({
  selector: 'app-update-shop-contactnumber',
  templateUrl: './updateshop-contactnumber.component.html',
  styleUrls: ['./updateshop-contactnumber.component.css']
})
export class UpdateShopContactnumberComponent {
  shopId: any = '';
  oldContactNumber: string = '';
  newContactNumber: string = '';
  message: string = '';
  error: string = '';

  constructor(private shopService: ShopService, private router: Router) { } // <-- Added router

  updateContactNumber(): void {
    if (!this.shopId || !this.oldContactNumber || !this.newContactNumber) {
      this.error = 'Please fill all fields.';
      this.message = '';
      return;
    }

    this.shopService.updateShopContactNumber(
      this.shopId,
      this.oldContactNumber.trim(),
      this.newContactNumber.trim()
    ).subscribe({
      next: (response: any) => {
        this.message = 'Contact number updated successfully.';
        this.error = '';
        this.router.navigate(['/landing']);
      },
      error: (error) => {
        if (error.status === 404) {
          this.error = 'Shop not found or contact number mismatch.';
        } else {
          this.error = 'Failed to update contact number.';
        }
        this.message = '';
      }
    });
  }
}
