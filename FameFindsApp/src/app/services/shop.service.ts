import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IShop } from '../Models/shop';
import { catchError, throwError } from 'rxjs';
import { IVendor } from '../Models/vendor';
import { ICity } from '../Models/city';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  shops: IShop[] = [];
  vendors: IVendor[] = [];
  cities: ICity[] = [];

  constructor(private _http: HttpClient) { }

  getAllShops() {
    return this._http
      .get<IShop[]>("https://localhost:7249/api/Shop")
      .pipe(catchError(this.errorHandler));
  }

  getShopsByName(shopName: string) {
    const params = new HttpParams().set('shopName', shopName);
    return this._http
      .get<IShop[]>(`https://localhost:7249/api/Shop/shopName`, { params })
      .pipe(catchError(this.errorHandler));
  }

  addShop(shop: IShop) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this._http
      .post<{ message: string }>(
        'https://localhost:7249/api/Shop/Register',
        shop,
        { headers }
      )
      .pipe(catchError(this.errorHandler));
  }



  updateShopContactNumber(shopId: number, contactNumber: string) {
    const params = new HttpParams()
      .set('shopId', shopId.toString())
      .set('contactNumber', contactNumber);
    return this._http
      .put('https://localhost:7249/api/Shop/contactNumber', null, { params })
      .pipe(catchError(this.errorHandler));
  }

  updateShopEmail(shopId: number, emailId: string) {
    const params = new HttpParams()
      .set('shopId', shopId.toString())
      .set('emailId', emailId);
    return this._http
      .put("https://localhost:7249/api/Shop/emailId", null, { params })
      .pipe(catchError(this.errorHandler));
  }

  updateShopIsOpenStatus(shopId: number, isOpen: boolean) {
    return this._http.put(
      `https://localhost:7249/api/Shop/isOpen?shopId=${shopId}&isOpen=${isOpen}`,
      {},
      { responseType: 'text' }
    );
  }

  deleteShop(shopId: number) {
    const params = new HttpParams().set('shopId', shopId.toString());
    return this._http
      .delete("https://localhost:7249/api/Shop", { params, responseType: 'text' })
      .pipe(catchError(this.errorHandler));
  }

  getShopsByProduct(productName: string) {
    const params = new HttpParams().set('productName', productName);
    return this._http.get<IShop[]>("https://localhost:7249/api/Shop/productName", { params });
  }


  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(() => error.message || "Server Error");
  }
}
