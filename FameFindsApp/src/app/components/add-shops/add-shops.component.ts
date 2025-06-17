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
  showToast: boolean = false;

  constructor(
    private fb: FormBuilder,
    private shopService: ShopService,
    private router: Router
  ) {
    this.vendorId = '';
  }

  ngOnInit(): void {

    const email = localStorage.getItem("Email");
    if (email) {
      this.shopService.getIdByEmail(email).subscribe({
        next: (response) => {
          const vendorId = response.vendorId;
          localStorage.setItem("vendorId", vendorId.toString());
          this.vendorId = vendorId.toString();
        },
        error: (err) => {
          console.error("Failed to fetch vendorId by email:", err);
        }
      });

    } else {
      console.warn("No email found in localStorage.");
    }


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
      closingTime: [''],
      
    });

    this.getAllCities();

    const storedLat = localStorage.getItem('selectedLat');
    const storedLng = localStorage.getItem('selectedLng');

    if (storedLat && storedLng) {
      const lat = parseFloat(storedLat);
      const lng = parseFloat(storedLng);

      this.shopForm.patchValue({
        latitude: lat,
        longitude: lng
      });

      this.fetchLocationDetails(lat, lng);

      localStorage.removeItem('selectedLat');
      localStorage.removeItem('selectedLng');

      this.showToast = true;
      setTimeout(() => {
        this.showToast = false;
      }, 3000);
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

    localStorage.removeItem('selectedLat');
    localStorage.removeItem('selectedLng');

    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          const lat = position.coords.latitude;
          const lng = position.coords.longitude;

          this.shopForm.patchValue({
            latitude: lat,
            longitude: lng
          });

          this.reverseGeocode(lat, lng);
        },
        (error) => {
          console.error('Location error:', error);
          alert('Failed to get your current location.');
        },
        {
          enableHighAccuracy: true,
          timeout: 10000
        }
      );
    } else {
      alert('Geolocation is not supported by your browser.');
    }
  }


  reverseGeocode(lat: number, lng: number): void {
    const url = `https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat=${lat}&lon=${lng}`;
    fetch(url)
      .then(response => response.json())
      .then(data => {
        const address = data.display_name || '';
        const city = data.address.city || data.address.town || data.address.village || '';
        const postcode = data.address.postcode || '';

        this.shopForm.patchValue({
          fullAddress: address,
          pincode: postcode,
          cityName: city
        });
      })
      .catch(err => console.error('Reverse geocoding error:', err));
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
      vendorId: localStorage.getItem('vendorId')
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

