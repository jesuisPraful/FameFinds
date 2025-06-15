//import { Component } from '@angular/core';
//import { ForgotpasswordService } from '../../services/forgotpassword.service';
//import { NgForm } from '@angular/forms';
//import { Router } from '@angular/router';

//@Component({
//  selector: 'app-reset-password',
//  templateUrl: './reset-password.component.html',
//  styleUrls: ['./reset-password.component.css']
//})
//export class ResetPasswordComponent {
//  newPassword: string = '';
//  confirmPassword: string = '';
//  message: string = '';
//  error: string = '';
//  showPasswords: boolean = false;
//  email: string = sessionStorage.getItem('forgotEmail') || '';

//  constructor(private _forgotService: ForgotpasswordService,private _router: Router) { }

//  reset(form: NgForm) {
//    if (form.invalid || this.newPassword !== this.confirmPassword) {
//      this.error = "Passwords do not match or form is invalid.";
//      return;
//    }

//    this._forgotService.resetPassword(this.email, this.newPassword, this.confirmPassword)
//      .subscribe(
//        () => {
//          this.message = "Password reset successful.";
//          this.error = "";
//          sessionStorage.removeItem('forgotEmail');
//          form.reset();
//          this._router.navigate(['/login']); 
//        },
//        () => {
//          this.error = "Failed to reset password.";
//          this.message = "";
//        }
//      );
//  }
//  toggleBothPasswordsVisibility() {
//    this.showPasswords = !this.showPasswords;
//  }
//}
