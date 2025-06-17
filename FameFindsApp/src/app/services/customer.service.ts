import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICustomer } from '../Models/customer';
import { catchError, throwError } from 'rxjs';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private _http: HttpClient) { }
  validateCredentials(email: string, password: string): Observable<any> {
    let param = "?email=" + email + "&passwordHash=" + password;
    let tempVar =this._http.post(`https://localhost:7249/api/Customer/login`+param,null).pipe(catchError(this.errorHandler))
    return tempVar;
  }
  getCustomerIdByEmail(email: string) {
    return this._http.get<number>(`https://localhost:7249/api/Customer/customerIdByEmail/${email}`).pipe(catchError(this.errorHandler))
  }
  errorHandler(error: HttpErrorResponse): Observable<never> {
    console.error(error);
    return throwError(error.message || "server Error");
  }

}
