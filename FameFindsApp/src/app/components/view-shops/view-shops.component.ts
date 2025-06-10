import { Component } from '@angular/core';
import { ShopService } from '../../services/shop.service';
import { Router } from '@angular/router';
import { IShop } from '../../Models/shop';

@Component({
  selector: 'app-view-shops',
  templateUrl: './view-shops.component.html',
  styleUrls: ['./view-shops.component.css']
})
export class ViewShopsComponent {
  shops: IShop[];

  constructor(private _shopService: ShopService, private _router: Router) {
    this.shops = [];
  }
}
