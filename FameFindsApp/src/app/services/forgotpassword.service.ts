import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ForgotpasswordService {

  constructor(private http: HttpClient) { }

  sendCustomerOtp(email: string) {
    return this.http.post('https://localhost:7249/api/Customer/request-otp', { email });
  }
}
