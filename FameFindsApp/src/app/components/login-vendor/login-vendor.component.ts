import { Component } from '@angular/core';
import { VendorService } from '../../services/vendor.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ShopService } from '../../services/shop.service';

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
    vendorId: string;
  constructor(private _vendorService: VendorService,private _router:Router,private shopService:ShopService) {
    this.message = "";
    this.showDiv = false;
    this.passwordLength = 0;
    this.highlightLogin = false;
    this.showPassword = false;
    this.vendorId = '';
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
            const email = localStorage.getItem("Email");
            if (email) {
              this.shopService.getIdByEmail(email).subscribe({
                next: (response) => {
                  const vendorId = response.vendorId;
                  localStorage.setItem("vendorId", vendorId.toString());
                  this.vendorId = vendorId.toString();
                },
                error: (err) => {
                  console.error("Failed to fetch vendorId by email:", err);
                }
              });

            } else {
              console.warn("No email found in localStorage.");
            }
 
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
