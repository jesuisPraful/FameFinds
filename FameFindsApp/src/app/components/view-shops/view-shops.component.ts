import { Component, OnInit} from '@angular/core';
import { IShop } from '../../models/shop';
import { ShopService } from '../../services/shop.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-shops',
  templateUrl: './view-shops.component.html',
  styleUrls: ['./view-shops.component.css']
})
export class ViewShopsComponent {
  shops: IShop[];
  showMessage: boolean = false;
  filteredShops: IShop[];
  searchByShopName: string = "";
  searchByVendorName: string = "";
  searchByCategory: string = "";
  searchByCity: string = "";
  showMsgDiv: boolean = false;
   

  constructor(private _shopService: ShopService, private _router: Router) {
    this.shops = [];
    this.filteredShops = [];

  }

  ngOnInit() {
    this._shopService.getAllShops().subscribe(
      (resSuccess) => {
        this.shops = resSuccess;
        this.filteredShops = this.shops;
        console.log(this.shops);

        if (this.shops.length == 0) {
          this.showMessage = true;
        }
      },
      (resError) => {
        this.showMessage = true;
        this.shops = [];
        this.filteredShops = [];
        console.log(resError);
      },
      () => { console.log("Get Shops executed successfully."); }
    );
  }

  searchShopsByShopName(shopName: string) {
    this._shopService.getShopsByName(shopName).subscribe(
      (resSuccess) => {
        this.shops = resSuccess;
        this.filteredShops = this.shops;
        console.log(this.shops);

        if (this.shops.length === 0) {
          this.showMessage = true;
        } else {
          this.showMessage = false;
        }
      },
      (resError) => {
        this.showMessage = true;
        this.shops = [];
        this.filteredShops = [];
        console.log(resError);
      },
      () => { console.log("Search Shops by Name executed successfully."); }
    );
  }


}
