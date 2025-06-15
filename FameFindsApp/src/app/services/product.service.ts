import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { IProduct } from '../Models/product';
import { ICategory } from '../Models/category';

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
    var tempVar = this._http.get<IProduct[]>("https://localhost:7249/api/Product/GetProducts")
      .pipe(catchError(this.errorHandler));
    return tempVar;
  }
  getAllCategories() {
    var tempVar = this._http.get<ICategory[]>("https://localhost:7249/api/Category")
      .pipe(catchError(this.errorHandler));
    return tempVar;
  }
  getProductsByCity(cityName: string): Observable<any[]> {
    return this._http.get<any[]>(`https://localhost:7249/api/Product/GetProductsByCityName?cityName=${cityName}`);
  }

  errorHandler(error:HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error")
  }

}
