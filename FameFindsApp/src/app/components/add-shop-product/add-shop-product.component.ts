import { Component } from '@angular/core';
import { ShopProductService } from '../../services/shopproduct.service';
import { Router } from '@angular/router';
import { ShopService } from '../../services/shop.service';
import { ProductService } from '../../services/product.service';
import { CityService } from '../../services/city.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-shop-product',
  templateUrl: './add-shop-product.component.html',
  styleUrls: ['./add-shop-product.component.css']
})
export class AddShopProductComponent {
  shopProductForm!: FormGroup;
  shopId!: number;
  cityId: number | null = null;
  products: any[] = [];
  constructor(private fb: FormBuilder,
    private shopProductService: ShopProductService,
    private shopService: ShopService,
    private productService: ProductService,
    private cityService: CityService,
    private router: Router
  ) { }
  ngOnInit(): void {
    const shopIdStr = localStorage.getItem('selectedShopId');
    this.shopId = Number(shopIdStr);

    this.shopService.getShopById(this.shopId).subscribe({
      next: (shop) => {
        this.cityId = shop.cityId;

        // Fetch products by cityId
        this.cityService.getCityById(this.cityId!).subscribe({
          next: (city) => {
            const cityName = city.cityName;
            this.productService.getProductsByCity(cityName).subscribe({
              next: (data) => this.products = data,
              error: (err) => console.error('Failed to fetch products:', err)
            });
          },
          error: (err) => console.error('Failed to fetch city:', err)
        });

      },
      error: (err) => {
        console.error('Error fetching shop:', err);
      }
    });

    this.shopProductForm = this.fb.group({
      productId: [null, Validators.required],
      price: [null, [Validators.required, Validators.min(1)]],
      stock: [null, [Validators.required, Validators.min(0)]]
    });
  }
  
  onSubmit(): void {
    if (this.shopProductForm.invalid) return;

    const shopProduct = {
      shopProductId: 0,
      shopId: this.shopId,
      productId:Number (this.shopProductForm.value.productId),
      price: this.shopProductForm.value.price,
      stock: this.shopProductForm.value.stock
    };

    this.shopProductService.addShopProduct(shopProduct).subscribe({
      next: () => {
        alert('Product added successfully!');
        this.shopProductForm.reset();
        this.router.navigate(['/vendor-shop-options'])
      },
      error: (err) => {
        console.error('Failed to add product:', err);
        alert('Failed to add product.');
      }
    });
  }
}



