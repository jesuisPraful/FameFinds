import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { IProduct } from '../../Models/product';
import { ICategory } from '../../Models/category';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-view-products',
  templateUrl: './view-products.component.html',
  styleUrls: ['./view-products.component.css']
})
export class ViewProductsComponent implements OnInit {
  products: IProduct[];
  categories: ICategory[];
  showMessage: boolean = false;
  cityName: string = '';
 

  constructor(private _productservice: ProductService,private _router:Router) {
    this.products = [];
    this.categories = [];
    const nav = this._router.getCurrentNavigation();
    this.cityName = nav?.extras?.queryParams?.['city'] || '';
  }

  goToFamousProducts(productName: string) {
    console.log("Navigating to:", productName);
    localStorage.setItem('selectedProduct', productName);
    this._router.navigate(['/view-shops']);

  }
ngOnInit() {
    const city = localStorage.getItem('selectedCity');
    if (city) {
      console.log("City in view-products:", city);
      this.cityName = city;
      document.title = 'Products for ' + city;

      this._productservice.getProductsByCity(city).subscribe(
        (res: IProduct[]) => {
          this.products = res;
        },
        (err: any) => {
          this.showMessage = true;
          this.products = [];
          console.error(err);
        }
      );
    } else {
      this._productservice.getAllProducts().subscribe(
        (resSuccess: IProduct[]) => {
          this.products = resSuccess;
        },
        (resError: any) => {
          this.showMessage = true;
          this.products = [];
          console.error(resError);
        },
        () => {
          console.log("Get products executed successfully!");
        }
      );
    }
  }


}
