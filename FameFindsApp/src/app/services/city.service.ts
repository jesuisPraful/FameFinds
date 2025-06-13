import { Injectable } from '@angular/core';
import { ICity } from '../models/city';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  cities: ICity[];

  constructor(private _http: HttpClient) {
    this.cities = [];
  }

  getAllCities() {
    return this._http
      .get<ICity[]>("https://localhost:7249/api/City")
      .pipe(catchError(this.errorHandler));
  }

  addCity(city: ICity) {
    return this._http
      .post("https://localhost:7249/api/City", city)
      .pipe(catchError(this.errorHandler));
  }

  getCityById(cityId: number) {
    return this._http
      .get<ICity>(`https://localhost:7249/api/City/id/${cityId}`)
      .pipe(catchError(this.errorHandler));
  }

  getCityByName(cityName: string) {
    return this._http
      .get<ICity>(`https://localhost:7249/api/City/name/${cityName}`)
      .pipe(catchError(this.errorHandler));
  }

  private errorHandler(error: HttpErrorResponse) {
    console.error('CityService Error:', error);
    return throwError(() => error);
  }
}
