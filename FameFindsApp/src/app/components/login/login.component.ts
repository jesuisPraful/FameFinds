import { Component } from '@angular/core';
import { CustomerService } from '../../services/customer.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  message: string;
  showDiv: boolean;
  passwordLength: number;
  highlightLogin: boolean;
  showPassword: boolean;
  constructor(private _customerService: CustomerService) {
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
            alert("Login Successfull!\n Welcome to FameFinds " + form.value.email);
            //sessionStorage.setItem("Role", resSuccess.toString());  in this if credentials are true api method is returning true are false but not the role of user wether it is vendor or customer so no need to store this.
            /*this._router.navigate(["/home"]);*/
          }
          else {
            alert("Invalid credentials! Please try again.")
            //this.message = "Invalid credentials! Please try again.";
            //this.showDiv = true;
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
