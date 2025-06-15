import { Component, OnInit } from '@angular/core';
import { IShop } from '../../Models/shop';
import { ShopService } from '../../services/shop.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-shops',
  templateUrl: './view-shops.component.html',
  styleUrls: ['./view-shops.component.css']
})
export class ViewShopsComponent implements OnInit {
  shops: IShop[] = [];
  filteredShops: IShop[] = [];
  searchByShopName: string = "";
  showMsgDiv: boolean = false;
  custLayout: boolean = false;
  commonLayout: boolean = false;
  role: string = "";
  userName: string = "";
  errMsg: string = "";

  selectedShop: IShop | null = null; // Used for map

  constructor(private _shopService: ShopService, private _router: Router) {
    this.role = sessionStorage.getItem("Role") || "NA";
    this.userName = sessionStorage.getItem("Email") || "NA";

    if (this.role.toLowerCase() == "admin" || this.role.toLowerCase() == "na") {
      this.commonLayout = true;
    } else {
      this.custLayout = true;
    }
  }

  ngOnInit(): void {
    this._shopService.getAllShops().subscribe(
      (resSuccess) => {
        this.shops = resSuccess;
        this.filteredShops = this.shops;

        if (this.shops.length === 0) {
          this.showMsgDiv = true;
        }
      },
      (resError) => {
        this.shops = [];
        this.filteredShops = [];
        this.showMsgDiv = true;
        console.log(resError);
      },
      () => {
        console.log("Get shops executed successfully.");
      }
    );
  }

  searchShop(shopName: string) {
    this.searchByShopName = shopName;

    if (shopName && shopName.trim() !== "") {
      this.filteredShops = this.shops.filter(
        shop => shop.shopName.toLowerCase().includes(shopName.toLowerCase())
      );
    } else {
      this.filteredShops = this.shops;
    }

    this.showMsgDiv = this.filteredShops.length === 0;
  }

  viewShopDetails(shop: IShop) {
    this._router.navigate(['/shop-details', shop.shopId]);
  }

  showMap(shop: IShop) {
    this.selectedShop = shop;

    setTimeout(() => {
      const map = new google.maps.Map(document.getElementById("map") as HTMLElement, {
        center: { lat: shop.latitude, lng: shop.longitude },
        zoom: 15
      });

      new google.maps.Marker({
        position: { lat: shop.latitude, lng: shop.longitude },
        map,
        title: shop.shopName
      });
    }, 0);
  }
}
