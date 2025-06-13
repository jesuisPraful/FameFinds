import { Component } from '@angular/core';
import { ForgotpasswordService } from '../../services/forgotpassword.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {
  email: string = '';
  message: string = '';
  error: string = '';
  loading: boolean = false;
  constructor(private _forgotService: ForgotpasswordService, private _router: Router) {

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
