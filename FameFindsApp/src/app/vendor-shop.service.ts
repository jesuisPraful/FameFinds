import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IShop } from './Models/shop';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VendorShopService {

  constructor(private _http: HttpClient) { }
  getShopsByVendorId(vendorId: number): Observable<IShop[]> {
    return this._http.get<IShop[]>(`https://localhost:7249/api/Shop/vendorId?vendorId=${vendorId}`);
  }

}
