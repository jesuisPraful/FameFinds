//import { Component, OnInit } from '@angular/core';
//import { ICity } from '../../Models/city';
//import { CityService } from '../../services/city.service';
//import { Router } from '@angular/router'; 


//@Component({
//  selector: 'app-view-cities',
//  templateUrl: './view-cities.component.html',
//  styleUrls: ['./view-cities.component.css']
//})
//export class ViewCitiesComponent implements OnInit {
//  cities: ICity[];
//  showMessage: boolean = false;
//  filteredCities: ICity[];
//  searchByName: string = "";


//  constructor(private _cityService: CityService, private _router: Router) {
//    this.cities = [];
//    this.filteredCities = [];
//  }

//  ngOnInit() {
//    this._cityService.getAllCities().subscribe(
//      (resSuccess: ICity[]) => {
//        this.cities = resSuccess;
//        this.filteredCities = this.cities;
//        console.log(this.cities);

//        if (this.cities.length === 0) {
//          this.showMessage = true;
//        }
//      },
//      (resError: any) => {
//        this.showMessage = true;
//        this.cities = [];
//        this.filteredCities = [];
//        console.log(resError);
//      },
//      () => {
//        console.log("Get Cities executed successfully.");
//      }
//    );

//  }

//  searchCity(cityName: string): void {
//    if (!cityName.trim()) {
//      this.filteredCities = this.cities;
//    } else {
//      const lowerName = cityName.toLowerCase();
//      this.filteredCities = this.cities.filter(city =>
//        city.cityName.toLowerCase().includes(lowerName)
//      );
//    }
//  }

//}

