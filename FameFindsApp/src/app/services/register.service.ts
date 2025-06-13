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

  //to check if he already a registered user
  checkEmailExists(email: string) {
    return this._http.get<boolean>(
      `https://localhost:7249/api/Customer/check-email?email=` + email
    );
  }
  // Add this to check wether user entered 10 digits of phone number
  validatePhoneNumber(phone: string): boolean {
    return /^\d{10}$/.test(phone);
  }


  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error");
  }
}
