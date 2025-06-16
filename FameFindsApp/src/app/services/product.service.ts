import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { IProduct } from '../Models/product';
import { ICategory } from '../Models/category';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  products: IProduct[];
  categories: ICategory[];

  constructor(private _http: HttpClient) {
    this.products = [];
    this.categories = [];
  }

  getAllProducts() {
    return this._http.get<IProduct[]>("https://localhost:7249/api/Product")
      .pipe(catchError(this.errorHandler));
  }

  getAllCategories() {
    return this._http.get<ICategory[]>("https://localhost:7249/api/Category")
      .pipe(catchError(this.errorHandler));
  }

  addProduct(product: IProduct) {
    return this._http.post<IProduct>("https://localhost:7249/api/Product", product)
      .pipe(catchError(this.errorHandler));
  }

  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error");
  }
}
