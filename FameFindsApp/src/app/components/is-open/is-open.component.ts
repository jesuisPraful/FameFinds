import { Component } from '@angular/core';
import { ShopService } from '../../services/shop.service';

@Component({
  selector: 'app-is-open',
  templateUrl: './is-open.component.html',
  styleUrls: ['./is-open.component.css']
})
export class IsOpenComponent {
  shopId!: number;
  isOpen: boolean = true;
  updateStatusMessage: string = '';

  constructor(private shopService: ShopService) { }

  updateIsOpenStatus() {
    if (!this.shopId) {
      this.updateStatusMessage = '❌ Shop ID is required.';
      return;
    }

    this.shopService.updateShopIsOpenStatus(this.shopId, this.isOpen).subscribe({
      next: () => this.updateStatusMessage = `Shop open status updated to: ${this.isOpen ? 'Open' : 'Closed'}`,
      error: err => this.updateStatusMessage = ` Failed to update: ${err}`
    });
  }

  toggleOpenStatus() {
    this.isOpen = !this.isOpen;
  }
}
