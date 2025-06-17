import { Component } from '@angular/core';
import { VendorService } from '../../services/vendor.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-vendor',
  templateUrl: './login-vendor.component.html',
  styleUrls: ['./login-vendor.component.css']
})
export class LoginVendorComponent {
  message: string;
  showDiv: boolean;
  passwordLength: number;
  highlightLogin: boolean;
  showPassword: boolean;
  constructor(private _vendorService: VendorService,private _router:Router) {
    this.message = "";
    this.showDiv = false;
    this.passwordLength = 0;
    this.highlightLogin = false;
    this.showPassword = false;
  }
  login(form: NgForm) {
    this._vendorService
      .validateCredentials(form.value.email, form.value.passwordHash)
      .subscribe(
        (resSuccess) => {
          console.log(resSuccess);
          //admin, customer or invalid credentials
          if (resSuccess == true) {
            localStorage.setItem("Email", form.value.email);
            sessionStorage.setItem("value", resSuccess);
 
            this._router.navigate(['/vendor-dashboard']);
          }
          else {
            alert("Invalid credentials! Please try again.")
            this.message = "Invalid credentials! Please try again.";
            this.showDiv = true;
          }
        },
        (resError) => {
          console.log(resError);
          this.message = "Some error occured.";
          this.showDiv = true;
        },//exception in calling the API i.e this method
        () => { console.log("validate credentials executed"); }
      );
  }

  onPasswordInput(event: any) {
    const value = event.target.value || '';
    this.passwordLength = value.length;
    this.highlightLogin = this.passwordLength >= 8;
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }


}
