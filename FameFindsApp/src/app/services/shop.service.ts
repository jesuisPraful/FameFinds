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

  getShopById(shopId: number) {
    return this._http
      .get<IShop>(`https://localhost:7249/api/Shop/${shopId}`)
      .pipe(catchError(this.errorHandler));
  }

  //getShopById(shopId: number) {
  //  const params = new HttpParams().set('shopId', shopId.toString());
  //  return this._http
  //    .get<IShop>("https://localhost:7249/api/Shop", { params })
  //    .pipe(catchError(this.errorHandler));
  //}


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



  updateShopContactNumber(shopId: number, contactNumber: string, nContactNumber: string) {
    const params = {
      shopId: shopId.toString(),
      contactNumber: contactNumber,
      nContactNumber: nContactNumber
    };

    return this._http.put(`https://localhost:7249/api/Shop/contactNumber`, null, { params });
  }

  updateShopName(shopId: number, shopName: string, nshopName: string) {
    const params = {
      shopId: shopId.toString(),
      shopName,
      nshopName
    };

    return this._http.put('https://localhost:7249/api/Shop/UpdateShopName', null, {
      params,
      responseType: 'text'  // <-- Critical for handling the plain string response
    }).pipe(catchError(this.errorHandler));
  }


  updateShopEmail(shopId: number, emailId: string, nemailId: string) {
    const params = {
      shopId: shopId.toString(),
      emailId: emailId,
      nemailId: nemailId
    };

    return this._http.put(`https://localhost:7249/api/Shop/emailId`, null, {
      params,
      responseType: 'text'  // expect string response from backend
    }).pipe(catchError(this.errorHandler));

  }

   

  updateShopIsOpenStatus(shopId: number, isOpen: boolean) {
    return this._http.put(
      `https://localhost:7249/api/Shop/isOpen?shopId=${shopId}&isOpen=${isOpen}`,
      {},
      { responseType: 'text' }
    ).pipe(catchError(this.errorHandler))
  }

  deleteShop(shopId: number) {
    const params = new HttpParams().set('shopId', shopId.toString());
    return this._http
      .delete("https://localhost:7249/api/Shop", { params, responseType: 'text' })
      .pipe(catchError(this.errorHandler));
  }

  getShopsByProduct(productName: string) {
    const params = new HttpParams().set('productName', productName);
    return this._http.get<IShop[]>("https://localhost:7249/api/Shop/productName", { params }).pipe(catchError(this.errorHandler))
  }

  getShopsByProductAndCityNames(productName: string, cityName: string) {
    const params = new HttpParams()
      .set('productName', productName)
      .set('cityName', cityName);
    return this._http.get<IShop[]>("https://localhost:7249/api/Shop/GetShops", { params }).pipe(catchError(this.errorHandler))

  }
  getCities()  {
    return this._http.get<any[]>("https://localhost:7249/api/City");
  }

  getIdByEmail(email:string) {
    const params = new HttpParams()
      .set('email', email)
    return this._http.get<any[]>("https://localhost:7249/api/Vendor/GetVendorByEmail", { params }).pipe(catchError(this.errorHandler))
  }

  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(() => error.message || "Server Error");
  }
}
