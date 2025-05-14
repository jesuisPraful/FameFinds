import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IShop } from '../models/shop';


@Injectable({
  providedIn: 'root'
})
export class ShopService {
  shops: IShop[];
  vendors:IVendor[];
  constructor(private _http: HttpClient) {
    this.shops = [];
    this.vendors = [];
  }


}
