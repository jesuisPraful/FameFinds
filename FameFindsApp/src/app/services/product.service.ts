import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IProduct } from '../Models/product';
import { ICategory } from '../Models/category';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  products: IProduct[] = [];
  categories: ICategory[] = [];

  constructor(private _http: HttpClient) { }

  getAllProducts(): Observable<IProduct[]> {
    return this._http.get<IProduct[]>("https://localhost:7249/api/Product/GetProducts")
      .pipe(catchError(this.errorHandler));
  }

  getAllCategories(): Observable<ICategory[]> {
    return this._http.get<ICategory[]>("https://localhost:7249/api/Category")
      .pipe(catchError(this.errorHandler));
  }

  addProduct(product: IProduct): Observable<IProduct> {
    return this._http.post<IProduct>("https://localhost:7249/api/Product", product)
      .pipe(catchError(this.errorHandler));
  }

  getProductsByCity(cityName: string): Observable<IProduct[]> {
    return this._http.get<IProduct[]>(
      `https://localhost:7249/api/Product/GetProductsByCity/GetProductsByCityName?cityName=${cityName}`
    ).pipe(catchError(this.errorHandler));
  }

  private errorHandler(error: HttpErrorResponse): Observable<never> {
    console.error(error);
    return throwError(() => error.message || "Server Error");
  }
}
