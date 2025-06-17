import { Component } from '@angular/core';
import { Register2Service } from '../../services/register2.service';
import { IVendor } from '../../Models/vendor';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration-page2',
  templateUrl: './registration-page2.component.html',
  styleUrls: ['./registration-page2.component.css']
})
export class RegistrationPage2Component {
    vendor: IVendor = {
    vendorName: "",
    email: "",
    phoneNumber: "",
    passwordHash: ""
  };
  emailExists: boolean = false;
  constructor(private _service: Register2Service, private _router: Router) {
  }

  checkEmailExists() {
    this._service.checkEmailExists(this.vendor.email).subscribe(
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
    this.phoneError = !this._service.validatePhoneNumber(this.vendor.phoneNumber);
  }


  registerUser(form: NgForm) {
    this._service.registerVendor(this.vendor).subscribe(
      (res) => {
        alert("Registered Successfully!")
        this._router.navigate(['/vendorlogin']);
        form.reset();
      },
      (err) => {
        alert("Registration Failed")
      },
      () => { console.log("Registration Executed Successfully"); }

    );
  }
}
