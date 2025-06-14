import { Component } from '@angular/core';
import { ShopService } from '../../services/shop.service';

@Component({
  selector: 'app-remove-shop',
  templateUrl: './remove-shop.component.html',
  styleUrls: ['./remove-shop.component.css']
})
export class RemoveShopComponent {
  shopId!: number;
  message: string = '';

  constructor(private shopService: ShopService) { }

  removeShop(): void {
    if (!this.shopId) {
      this.message = 'Shop ID is required.';
      return;
    }

    this.shopService.deleteShop(this.shopId).subscribe({
      next: (res) => {
        this.message = `Shop with ID ${this.shopId} removed successfully.`;
      },
      error: (err) => {
        this.message = `Failed to remove shop: ${err}`;
      }
    });
  }
}
