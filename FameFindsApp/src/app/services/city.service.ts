import { Injectable } from '@angular/core';
import { ICity } from '../Models/city';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  cities: ICity[];

  constructor(private _http: HttpClient,private _router:Router) {
    this.cities = [];
  }

  getAllCities(): Observable<ICity[]> {
    return this._http.get<ICity[]>('https://localhost:7249/api/City');
  }
  goToProducts(cityName: string) {
    this._router.navigate(['/view-products', { queryParams: { city: cityName } }]);
  }

  addCity(city: ICity): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'text/plain'
    });
    
    // Send both fields
    const body = {
      cityId: city.cityId,
      cityName: city.cityName
    };

    return this._http
      .post('https://localhost:7249/api/City', body, {
        headers,
        responseType: 'text' // because backend returns plain text
      })
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
  deleteCity(cityId: number): Observable<any> {
    return this._http
      .delete(`https://localhost:7249/api/City/${cityId}`, {
        responseType: 'text' // because the backend returns plain text
      })
      .pipe(catchError(this.errorHandler));
  }


  private errorHandler(error: HttpErrorResponse) {
    console.error('CityService Error:', error);
    return throwError(() => error);
  }
}
