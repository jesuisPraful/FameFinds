import { Component,ViewEncapsulation } from '@angular/core';
import { RegisterService } from '../../services/register.service';
import { ICustomer } from '../../Models/customer';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.css'],
})
export class RegistrationPageComponent {
  user: ICustomer = {
    fullname: "",
    email: "",
    phoneNumber: "",
    password: ""
  };
    emailExists: boolean = false;
  constructor(private _service: RegisterService, private _router: Router) { }

  checkEmailExists() {
    this._service.checkEmailExists(this.user.email).subscribe(
      (exists: boolean) => {
        this.emailExists = exists;
      },
      (err) => {
        console.error("Email check failed:", err);
        this.emailExists = false;
      }
    );
  }

  phoneError: boolean = false;
  phoneTouched: boolean = false;

  validatePhoneNumber() {
    this.phoneTouched = true;
    this.phoneError = !this._service.validatePhoneNumber(this.user.phoneNumber);
  }


  registerUser(form: NgForm) {
    this._service.registerCustomer(this.user).subscribe(
        (res) => {
        alert("Registered Successfully!")
        this._router.navigate(['/login'])
        form.reset();
      },
        (err) => {
        alert("Registration Failed")
        },
      () => { console.log("Registration Executed Successfully"); }
      
    );
  }

}
