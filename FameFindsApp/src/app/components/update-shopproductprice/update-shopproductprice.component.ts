import { Component } from '@angular/core';
import { ShopProductService } from 'src/app/services/shopproduct.service';

@Component({
  selector: 'app-update-shopproductprice',
  templateUrl: './update-shopproductprice.component.html',
  styleUrls: ['./update-shopproductprice.component.css']
})
export class UpdateShopproductpriceComponent {

  shopProductId: number = 0;
  newPrice: number = 0;
  message: string = '';
  error: string = '';

  constructor(private shopProductService: ShopProductService) { }

  updatePrice(): void {
    if (this.shopProductId <= 0 || this.newPrice < 0) {
      this.error = 'Please enter valid Shop Product ID and Price.';
      this.message = '';
      return;
    }

    this.shopProductService.updatePrice(this.shopProductId, this.newPrice).subscribe({
      next: (res) => {
        this.message = res;
        this.error = '';
      },
      error: () => {
        this.error = 'Failed to update price.';
        this.message = '';
      }
    });
  }
}
