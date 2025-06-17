import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ShopService } from '../../services/shop.service';
import { Router } from '@angular/router';
import { ICity } from '../../Models/city';
import { IShop } from '../../Models/shop';

@Component({
  selector: 'app-add-shops',
  templateUrl: './add-shops.component.html',
  styleUrls: ['./add-shops.component.css']
})
export class AddShopsComponent implements OnInit {
  shopForm!: FormGroup;
  cities: ICity[] = [];
  cityNotInDb: boolean = false;
  message: string = '';
  loading = false;
  vendorId: string;

  constructor(
    private fb: FormBuilder,
    private shopService: ShopService,
    private router: Router
  ) {
    this.vendorId = '';
  }

  ngOnInit(): void {
    this.shopForm = this.fb.group({
      shopName: ['', Validators.required],
      emailId: ['', [Validators.required, Validators.email]],
      cityId: ['', Validators.required],
      pincode: ['', Validators.required],
      contactNumber: ['', Validators.required],
      fullAddress: ['', Validators.required],
      latitude: [''],
      longitude: [''],
      isOpen: [true],
      openingTime: [''],
      closingTime: ['']
    });

    this.getAllCities();

    const storedLat = localStorage.getItem('selectedLat');
    const storedLng = localStorage.getItem('selectedLng');

    if (storedLat && storedLng) {
      this.shopForm.patchValue({
        latitude: parseFloat(storedLat),
        longitude: parseFloat(storedLng)
      });

      // Auto-fetch address if lat/lng exists from map-picker
      this.fetchLocationDetails(parseFloat(storedLat), parseFloat(storedLng));
    }
  }

  getAllCities(): void {
    this.shopService.getCities().subscribe({
      next: (res) => {
        this.cities = res;
      },
      error: (err) => {
        console.error('Error fetching cities:', err);
      }
    });
  }

  useMyLocation(): void {
    if (!navigator.geolocation) {
      this.message = 'Geolocation is not supported by your browser.';
      return;
    }

    navigator.geolocation.getCurrentPosition(
      (position) => {
        const lat = position.coords.latitude;
        const lng = position.coords.longitude;

        this.shopForm.patchValue({
          latitude: lat,
          longitude: lng
        });

        this.fetchLocationDetails(lat, lng);
      },
      () => {
        this.message = 'Unable to retrieve your location.';
      }
    );
  }

  fetchLocationDetails(lat: number, lng: number): void {
    fetch(`https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${lng}`)
      .then((res) => res.json())
      .then((data) => {
        const address = data.address;
        const fullAddress = data.display_name || '';
        const pincode = address.postcode || '';
        const cityFromLocation = address.city || address.town || address.village || '';

        this.shopForm.patchValue({
          fullAddress: fullAddress,
          pincode: pincode
        });

        const matchedCity = this.cities.find(
          (c) => c.cityName.toLowerCase() === cityFromLocation.toLowerCase()
        );

        if (matchedCity) {
          this.cityNotInDb = false;
          this.shopForm.patchValue({ cityId: matchedCity.cityId });
        } else {
          this.cityNotInDb = true;
          alert("🌍 We're expanding and will be serving in this area soon!\nPlease select a city manually.");
        }
      })
      .catch((err) => {
        console.error('Error fetching location details:', err);
        this.message = 'Could not fetch address from coordinates.';
      });
  }

  navigateToMap(): void {
    this.router.navigate(['/map-picker']);
  }

  addShop(): void {
    if (this.shopForm.invalid) {
      this.message = 'Please fill all required fields.';
      return;
    }

    const newShop: IShop = {
      ...this.shopForm.value,
      shopId: 0,
      createdAt: new Date(),
      vendorId: this.shopService.getIdByEmail(localStorage.getItem("Email") ?? '')
    };

    this.loading = true;

    this.shopService.addShop(newShop).subscribe({
      next: () => {
        this.loading = false;
        this.message = 'Shop added successfully!';
        this.router.navigate(['/view-shops']);
      },
      error: (error) => {
        this.loading = false;
        console.error('Error adding shop:', error);
        this.message = 'Error adding shop. Please try again.';
      }
    });
  }
}
