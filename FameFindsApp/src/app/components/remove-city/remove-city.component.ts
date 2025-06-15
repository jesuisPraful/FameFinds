//import { Component } from '@angular/core';
//import { CityService } from '../../services/city.service';

//@Component({
//  selector: 'app-remove-city',
//  templateUrl: './remove-city.component.html',
//  styleUrls: ['./remove-city.component.css']
//})
//export class RemoveCityComponent {
//  cityId: number = 0;
//  message: string = '';
//  isSuccess: boolean = false;

//  constructor(private cityService: CityService) { }

//  deleteCity() {
//    if (!this.cityId) {
//      this.message = 'Please enter a valid City ID.';
//      this.isSuccess = false;
//      return;
//    }

//    this.cityService.deleteCity(this.cityId).subscribe({
//      next: (res) => {
//        this.message = res;
//        this.isSuccess = true;
//        this.cityId = 0;
//      },
//      error: (err) => {
//        this.message = 'Failed to delete city.';
//        this.isSuccess = false;
//      }
//    });
//  }
//}
