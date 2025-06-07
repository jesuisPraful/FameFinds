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
    let response = this._http.post("https://localhost:7249/api/Vendor/AddVendor" ,vendor, {
      responseType: 'text'
    }).pipe(catchError(this.errorHandler))
    return response;
  }
  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error");
  }
}
