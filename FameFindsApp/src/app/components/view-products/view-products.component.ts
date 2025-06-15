import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { ICategory } from '../../Models/category';
import { IProduct } from '../../Models/product';

@Component({
  selector: 'app-view-products',
  templateUrl: './view-products.component.html',
  styleUrls: ['./view-products.component.css']
})
export class ViewProductsComponent implements OnInit {
  products: IProduct[];
  categories: ICategory[];
  showMessage: boolean = false;

  constructor(private _productservice: ProductService) {
    this.products = [];
    this.categories = [];
  }

  ngOnInit() {
    this._productservice
      .getAllProducts()
      .subscribe(
        (resSuccess) => {
          this.products = resSuccess;
        },
        (resError) => {
          this.showMessage = true;
          this.products = [];
          console.log(resError);
        },
        () => { console.log("Get products executed successfully!"); }
        
    );
  }
}
