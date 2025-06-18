import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IVendor } from '../Models/vendor';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Register2Service {
  vendor: IVendor[];
  constructor(private _http: HttpClient) {
    this.vendor = [];
  }
  registerVendor(vendor: IVendor) {
    let response = this._http.post("https://localhost:7249/api/Vendor/AddVendor/Register" ,vendor, {
      responseType: 'text'
    }).pipe(catchError(this.errorHandler))
    return response;
  }

  //to check if he already a registered user
  checkEmailExists(email: string) {
    return this._http.get<boolean>(
      `https://localhost:7249/api/Vendor/CheckEmailExists/check-email?email=` + email
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
