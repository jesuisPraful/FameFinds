import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ForgotpasswordService {
  constructor(private http: HttpClient) {

  }
  requestOtp(email: string) {
    let response = this.http.post("https://localhost:7249/api/Customer/request-otp", { email }, { responseType: 'text' }
).pipe(catchError(this.errorHandler));
    return response;
  }

  verifyOtp(email: string, otp: string) {
    const body = { email, otp };
    return this.http.post("https://localhost:7249/api/Customer/verify-otp", body, { responseType: 'text' })
      .pipe(catchError(this.errorHandler));
  }

  resetPassword(email: string, newPassword: string, confirmPassword: string) {
    const body = { email, newPassword, confirmPassword };
    return this.http.post("https://localhost:7249/api/Customer/reset-password", body, { responseType: 'text' })
      .pipe(catchError(this.errorHandler));
  }

  errorHandler(error: HttpErrorResponse): Observable<never> {
    console.error(error);
    return throwError(error.message || "server Error");
  }
}
