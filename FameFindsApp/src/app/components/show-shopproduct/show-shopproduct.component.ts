import { Component, OnInit } from '@angular/core';
import { ShopProduct, ShopProductService } from 'src/app/services/shopproduct.service';

@Component({
  selector: 'app-show-shopproduct',
  templateUrl: './show-shopproduct.component.html',
  styleUrls: ['./show-shopproduct.component.css']
})
export class ShowShopproductComponent implements OnInit {

  shopProducts: ShopProduct[] = [];
  shopId: any = '';
  message: string = '';
  error: string = '';

  constructor(private shopProductService: ShopProductService) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts(): void {
    this.shopId = Number(localStorage.getItem("selectedShopId"));
    this.shopProductService.getProductsByShopId(this.shopId).subscribe({
      next: (products) => {
        this.shopProducts = products;
        this.message = '';
        this.error = '';
      },
      error: () => {
        this.error = 'Failed to load products.';
        this.shopProducts = [];
      }
    });
  }

  // ✅ ADD THIS METHOD
  deleteProduct(shopProductId: number): void {
    if (confirm('Are you sure you want to delete this product?')) {
      this.shopProductService.deleteShopProduct(shopProductId).subscribe({
        next: (res: string) => {
          this.message = res;
          this.error = '';
          this.getProducts(); // refresh list

        },
        error: () => {
          this.error = 'Delete failed.';
          this.message = '';
        }
      });
    }
  }
}
