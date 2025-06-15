import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface IShop {
  shopId: number;
  shopName: string;
  emailId: string;
  cityId: number;
  pincode: string;
  contactNumber: string;
  fullAddress: string;
  latitude: number;
  longitude: number;
  openingTime: Date;
  closingTime: Date;
  isOpen: boolean;
  createdAt: Date;
  vendorId: number;
}

@Component({
  selector: 'app-view-shops',
  templateUrl: './view-shops.component.html',
  styleUrls: ['./view-shops.component.css']
})
export class ViewShopsComponent implements OnInit {
  shops: IShop[] = [];
  productName: string = 'iPhone';
  cityName: string = 'delhi';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http
      .get<IShop[]>(`https://localhost:7249/api/Shop/GetShops?productName=${this.productName}&cityName=${this.cityName}`)
      .subscribe((data) => {
        this.shops = data;
      });
  }
}
