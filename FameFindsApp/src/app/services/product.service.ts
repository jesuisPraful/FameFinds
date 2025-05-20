import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { ICategory } from '../Models/Category';
import { IProduct } from '../Models/Product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  products: IProduct[];
  categories:ICategory[];

  constructor(private _http:HttpClient) {
    this.products = [];
    this.categories= [];
  }

  getAllProducts(){
    var tempVar = this._http.get<IProduct[]>("https://localhost:7249/api/Product")
      .pipe(catchError(this.errorHandler));
    return tempVar;
  }
  getAllCategories() {
    var tempVar = this._http.get<ICategory[]>("https://localhost:7249/api/Category")
      .pipe(catchError(this.errorHandler));
    return tempVar;
  }
  errorHandler(error:HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error")
  }

}
