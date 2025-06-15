//import { Component } from '@angular/core';
//import { ForgotpasswordService } from '../../services/forgotpassword.service';
//import { NgForm } from '@angular/forms';
//import { Router } from '@angular/router';

//@Component({
//  selector: 'app-otp-verification',
//  templateUrl: './otp-verification.component.html',
//  styleUrls: ['./otp-verification.component.css']
//})
//export class OtpVerificationComponent {
//  otp: string = '';
//  email: string = sessionStorage.getItem('forgotEmail') || '';
//  message: string = '';
//  error: string = '';
//  isLoading: boolean = false;
//  constructor(private _forgotService: ForgotpasswordService, private _router: Router) { }

//  verifyOtp(form: NgForm) {
//    if (form.invalid || !this.email) {
//      this.error = 'Invalid input or session expired.';
//      return;
//    }

//    this.isLoading = true;

//    this._forgotService.verifyOtp(this.email, this.otp).subscribe(
//      (resSuccess) => {
//        this.message = 'OTP verified successfully.';
//        this.error = '';
//        this.isLoading = false;
//        this._router.navigate(['/reset-password']); 
//      },
//      (err) => {
//        console.error("Verify OTP failed:", err);
//        this.error = 'Invalid OTP or verification failed.';
//        this.message = '';
//        this.isLoading = false;
//      }
//    );
//  }
//}
