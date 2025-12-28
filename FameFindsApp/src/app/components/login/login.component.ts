import { Component } from '@angular/core';
import { CustomerService } from '../../services/customer.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  message: string;
  showDiv: boolean;
  passwordLength: number;
  highlightLogin: boolean;
  showPassword: boolean;
  constructor(private _customerService: CustomerService,private _router:Router) {
    this.message = "";
    this.showDiv = false;
    this.passwordLength = 0;
    this.highlightLogin = false;
    this.showPassword = false;

  }
  login(form: NgForm) {
    this._customerService
      .validateCredentials(form.value.email, form.value.password)
      .subscribe(
        (resSuccess) => {
          console.log(resSuccess);
          //admin, customer or invalid credentials
          if (resSuccess==true) {
            sessionStorage.setItem("Email", form.value.email);
            sessionStorage.setItem("value", resSuccess);

            this._customerService.getCustomerIdByEmail(form.value.email).subscribe({
              next: (customerId: number) => {
                if (customerId > 0) {
                  localStorage.setItem('customerId', customerId.toString());
                } else {
                  console.warn("Customer ID not found for this email.");
                }
                this._router.navigate(['/view-cities']);
              },
              error: (err) => {
                console.error("Failed to fetch customerId:", err);
                alert("Login succeeded, but failed to fetch customer ID.");
                this._router.navigate(['/view-cities']);
              }
            });
          } else {
            alert("Invalid credentials! Please try again.");
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
