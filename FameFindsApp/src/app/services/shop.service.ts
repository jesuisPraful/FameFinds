import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { ErrorHandler, Injectable } from '@angular/core';
import { IShop } from '../models/shop';
import { catchError, throwError } from 'rxjs';
import { IVendor } from '../Models/vendor';
import { ICity } from '../models/city';


@Injectable({
  providedIn: 'root'
})
export class ShopService {
  shops: IShop[];
  vendors: IVendor[];
  cities: ICity[];
  constructor(private _http: HttpClient){
    this.shops = [];
    this.vendors = [];
    this.cities = [];
  }
  getAllShops() {
    var tempVar = this._http
      .get<IShop[]>("https://localhost:7249/api/Shop")
      .pipe(catchError(this.errorHandler));
    return tempVar;
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
      .post("https://localhost:7249/api/Shop/Register", shop, { headers })
      .pipe(catchError(this.errorHandler));
  }


   
  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error");
  }

}
