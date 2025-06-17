import { Component } from '@angular/core';
import { ShopService } from '../../services/shop.service';

@Component({
  selector: 'app-updateshop-name',
  templateUrl: './updateshop-name.component.html',
  styleUrls: ['./updateshop-name.component.css']
})
export class UpdateshopNameComponent {
  shopId: any = '';
  currentShopName: string = '';
  newShopName: string = '';
  message: string = '';
  error: string = '';

  constructor(private shopService: ShopService) { }

  updateShopName(): void {
    if (!this.shopId || !this.currentShopName || !this.newShopName) {
      this.error = 'All fields are required.';
      this.message = '';
      return;
    }

    this.shopService.updateShopName(
      this.shopId,
      this.currentShopName.trim(),
      this.newShopName.trim()
    ).subscribe({
      next: (res) => {
        this.message = 'Shop name updated successfully';
        this.error = '';
        console.log('Success:', res);
      },
      error: (err) => {
        this.error = 'Failed to update shop name';
        this.message = '';
        console.error('Error:', err);
      }
    });
  }
}
