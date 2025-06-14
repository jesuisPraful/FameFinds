import { Component } from '@angular/core';
import { ICity } from '../../Models/city';
import { CityService } from '../../services/city.service';


@Component({
  selector: 'app-add-city',
  templateUrl: './add-city.component.html',
  styleUrls: ['./add-city.component.css']
})
export class AddCityComponent {
  city: ICity = {
    cityId: 0,
    cityName: ''
  };

  message: string = '';

  constructor(private cityService: CityService) { }

  onSubmit(): void {
    this.cityService.addCity(this.city).subscribe({
      next: (res) => {
        this.message = res;
        this.city = { cityId: 0, cityName: '' }; // reset form
      },
      error: (err) => {
        this.message = 'Failed to add city.';
        console.error(err);
      }
    });
  }
}
