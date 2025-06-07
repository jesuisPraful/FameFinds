import { Injectable } from '@angular/core';
import { ICustomer } from '../Models/customer';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  customer: ICustomer[];

  constructor(private _http: HttpClient) {
    this.customer = [];
  }
  registerCustomer(customer: ICustomer) {
    let response = this._http.post("https://localhost:7249/api/Customer/Register", customer, {
      responseType: 'text' }).pipe(catchError(this.errorHandler))
    return response;
  }
  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error");
  }
}
