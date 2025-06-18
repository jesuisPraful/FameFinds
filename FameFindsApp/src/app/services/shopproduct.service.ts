import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { IShopProduct } from '../Models/shopProduct';

export type ShopProduct = IShopProduct;

@Injectable({
  providedIn: 'root'
})
export class ShopProductService {

  private baseUrl = 'https://localhost:7249/api/ShopProduct'; // adjust to match your backend base URL

  constructor(private http: HttpClient) { }



  getProductsByShopId(shopId: number): Observable<ShopProduct[]> {
    return this.http.get<ShopProduct[]>(`https://localhost:7249/api/ShopProduct/shop/${shopId}`)
      .pipe(catchError(this.handleError));
  }

  //addShopProduct(product: ShopProduct): Observable<ShopProduct> {
  //  return this.http.post<ShopProduct>(`${this.baseUrl}/add`, product)
  //    .pipe(catchError(this.handleError));
  //}

 
  addShopProduct(shopProduct: any): Observable<any> {
    return this.http.post('https://localhost:7249/api/ShopProduct/add', shopProduct);
  }


  updateShopProduct(id: number, product: ShopProduct): Observable<string> {
    return this.http.put<string>(`${this.baseUrl}/update/${id}`, product)
      .pipe(catchError(this.handleError));
  }

  updatePrice(id: number, price: number): Observable<string> {
    return this.http.put<string>(`${this.baseUrl}/update/price/${id}`, price)
      .pipe(catchError(this.handleError));
  }

  updateStock(id: number, stock: number): Observable<string> {
    return this.http.put<string>(`${this.baseUrl}/update/stock/${id}`, stock)
      .pipe(catchError(this.handleError));
  }

  deleteShopProduct(id: number): Observable<string> {
    return this.http.delete<string>(`${this.baseUrl}/delete/${id}`)
      .pipe(catchError(this.handleError));
  }

  
  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      // Client-side
      errorMessage = `Client-side error: ${error.error.message}`;
    } else {
      // Server-side
      errorMessage = `Server returned code ${error.status}, body was: ${error.error}`;
    }
    return throwError(() => new Error(errorMessage));
  }
}
