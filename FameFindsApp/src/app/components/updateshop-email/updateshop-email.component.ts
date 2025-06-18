import { Component } from '@angular/core';
import { Router } from '@angular/router'; // <-- Import Router
import { ShopService } from '../../services/shop.service';

@Component({
  selector: 'app-updateshop-email',
  templateUrl: './updateshop-email.component.html',
  styleUrls: ['./updateshop-email.component.css']
})
export class UpdateshopEmailComponent {
  shopId: any = '';
  currentEmail: string = '';
  newEmail: string = '';
  message: string = '';
  error: string = '';

  constructor(private shopService: ShopService, private router: Router) { } // <-- Inject Router

  updateEmail(): void {
    if (!this.shopId || !this.currentEmail || !this.newEmail) {
      this.error = 'All fields are required.';
      this.message = '';
      return;
    }

    this.shopService.updateShopEmail(
      this.shopId,
      this.currentEmail.trim(),
      this.newEmail.trim()
    ).subscribe({
      next: (res) => {
        this.message = 'Shop email updated successfully';
        this.error = '';
        console.log('Success:', res);
        this.router.navigate(['/landing']); 
      },
      error: (err) => {
        this.error = 'Failed to update shop email';
        this.message = '';
        console.error('Error:', err);
      }
    });
  }
}
