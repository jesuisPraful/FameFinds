import { Component } from '@angular/core';
import { ForgotpasswordvendorService } from '../../services/forgotpasswordvendor.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-forgot-password-vendor',
  templateUrl: './forgot-password-vendor.component.html',
  styleUrls: ['./forgot-password-vendor.component.css']
})
export class ForgotPasswordVendorComponent {

  email: string = '';
  message: string = '';
  error: string = '';
  loading: boolean = false;
  constructor(private _forgotService: ForgotpasswordvendorService, private _router: Router) {

  }
  sendOtp(form: NgForm) {
    if (form.invalid) return;

    this.loading = true;
    sessionStorage.setItem('forgotEmail', this.email);

    this._forgotService.requestOtp(this.email)
      .subscribe(
        (resSuccess) => {
          this.message = 'OTP sent to your email.';
          this.error = '';
          form.reset();
          this._router.navigate(['/verify-otp']);
        },
        (resError) => {
          this.error = 'Failed to send OTP. Please try again.';
          this.message = '';
          console.error(resError);
        },
        () => {
          console.log("SendOTP Method is working fine");
          this.loading = false;
        }
      )
  }
}
