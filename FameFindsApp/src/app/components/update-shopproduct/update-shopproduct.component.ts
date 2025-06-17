import { Component } from '@angular/core';

@Component({
  selector: 'app-update-shopproduct',
  templateUrl: './update-shopproduct.component.html',
  styleUrls: ['./update-shopproduct.component.css']
})
export class UpdateShopproductComponent {
  showStockComponent: boolean = false;
  showPriceComponent: boolean = false;

  showComponent(type: string): void {
    this.showStockComponent = type === 'stock';
    this.showPriceComponent = type === 'price';
  }
}
