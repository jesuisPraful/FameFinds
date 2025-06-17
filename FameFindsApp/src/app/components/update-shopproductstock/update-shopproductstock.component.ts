import { Component } from '@angular/core';
import { ShopProductService } from 'src/app/services/shopproduct.service';

@Component({
  selector: 'app-update-shopproductstock',
  templateUrl: './update-shopproductstock.component.html',
  styleUrls: ['./update-shopproductstock.component.css']
})
export class UpdateShopproductstockComponent {

  shopProductId: number = 0;
  newStock: number = 0;
  message: string = '';
  error: string = '';

  constructor(private shopProductService: ShopProductService) { }

  updateStock(): void {
    if (this.shopProductId <= 0 || this.newStock < 0) {
      this.error = 'Please enter valid Shop Product ID and Stock.';
      this.message = '';
      return;
    }

    this.shopProductService.updateStock(this.shopProductId, this.newStock).subscribe({
      next: (res) => {
        this.message = res;
        this.error = '';
      },
      error: () => {
        this.error = 'Failed to update stock.';
        this.message = '';
      }
    });
  }
}
