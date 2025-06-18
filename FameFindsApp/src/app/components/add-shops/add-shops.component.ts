import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IShop } from '../../Models/shop';
import { ShopService } from '../../services/shop.service';
import { MapInfoWindow, MapMarker } from '@angular/google-maps';

@Component({
  selector: 'app-add-shops',
  templateUrl: './add-shops.component.html',
  styleUrls: ['./add-shops.component.css']
})
export class AddShopsComponent {
  newShop: IShop = {
    shopId: 0,
    shopName: '',
    emailId: '',
    cityId: 0,
    pincode: '',
    contactNumber: '',
    fullAddress: '',
    latitude: 0,
    longitude: 0,
    isOpen: false,
    createdAt: new Date(),
    vendorId: 0
  };

  message: string = '';
  center: google.maps.LatLngLiteral = { lat: 20.5937, lng: 78.9629 };
  zoom = 5;
  markerPosition: google.maps.LatLngLiteral | null = null;

  constructor(public _shopService: ShopService, public _router: Router) { }

  onMapClick(event: google.maps.MapMouseEvent): void {
    if (event.latLng) {
      const lat = event.latLng.lat();
      const lng = event.latLng.lng();
      this.newShop.latitude = lat;
      this.newShop.longitude = lng;
      this.markerPosition = { lat, lng };
    }
  }

  addShop(): void {
    if (!this.newShop.latitude || !this.newShop.longitude) {
      this.message = 'Please select a location on the map.';
      return;
    }

    this.newShop.createdAt = new Date();

    this._shopService.addShop(this.newShop).subscribe({
      next: (response) => {
        console.log('Shop added successfully:', response);
        this.message = 'Shop added successfully!';
        this._router.navigate(['/view-shops']);
      },
      error: (error) => {
        console.error('Error adding shop:', error);
        if (error.error instanceof ErrorEvent) {
          // Client-side error
          this.message = 'Network error occurred. Please check your connection.';
        } else {
          // Server-side error
          if (error.status === 0) {
            this.message = 'Could not connect to server. Please try again later.';
          } else if (error.error) {
            // Try to get server error message
            try {
              const errorObj = typeof error.error === 'string' ? JSON.parse(error.error) : error.error;
              this.message = errorObj.message || 'An unexpected error occurred.';
            } catch (e) {
              this.message = error.statusText || 'An unexpected error occurred.';
            }
          } else {
            this.message = `Server returned code ${error.status}`;
          }
        }
      }
    });
  }
}

