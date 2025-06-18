import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VendorService {

  constructor(private _http: HttpClient) { }
  validateCredentials(email: string, passwordHash: string): Observable<any> {
    let param = "?email=" + email + "&passwordHash=" + passwordHash;
    let tempVar = this._http.post(`https://localhost:7249/api/Vendor/Login/login` + param, null).pipe(catchError(this.errorHandler))
    return tempVar;
  }

  getVendorRatings(): Observable<any[]> {
    return this._http.get<any[]>("https://localhost:7249/api/Vendor/GetRatings");
  }

  errorHandler(error: HttpErrorResponse): Observable<never> {
    console.error(error);
    return throwError(error.message || "server Error");
  }
}
