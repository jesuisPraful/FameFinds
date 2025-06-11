import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { ErrorHandler, Injectable } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { IShop } from '../Models/shop';
import { IVendor } from '../Models/Vendor'; }



@Injectable({
  providedIn: 'root'
})
export class ShopService {
  shops: IShop[];
  vendors: IVendor[];
  constructor(private _http: HttpClient){
    this.shops = [];
    this.vendors = [];
  }
  getAllShops() {
    var tempVar = this._http
      .get<IShop[]>("https://localhost:7249/api/Shop")
      .pipe(catchError(this.errorHandler));

    return tempVar;
  }

   
  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error");
  }

}
