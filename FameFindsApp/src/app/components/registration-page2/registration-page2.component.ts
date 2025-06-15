import { Component } from '@angular/core';

//@Component({
//  selector: 'app-registration-page2',
//  templateUrl: './registration-page2.component.html',
//  styleUrls: ['./registration-page2.component.css']
//})
//export class RegistrationPage2Component {
//  vendor: IVendor = {
//    vendorId: "",
//    vendorName: "",
//    email: "",
//    phoneNumber: "",
//    passwordHash: ""
//  };
//  emailExists: boolean = false;
//  constructor(private _service: Register2Service) { }
//@Component({
//  selector: 'app-registration-page2',
//  templateUrl: './registration-page2.component.html',
//  styleUrls: ['./registration-page2.component.css']
//})
//export class RegistrationPage2Component {
//    user: IVendor = {
//    vendorName: "",
//    email: "",
//    phoneNumber: "",
//    passwordHash: ""
//  };
//  emailExists: boolean = false;
//  constructor(private _service: Register2Service) {
//  }

//  checkEmailExists() {
//    this._service.checkEmailExists(this.vendor.email).subscribe(
//      (exists: boolean) => {
//        this.emailExists = exists;
//      },
//      (err) => {
//        console.error("Email check failed:", err);
//        this.emailExists = false;
//      }
//    );
//  }
//  checkEmailExists() {
//    this._service.checkEmailExists(this.user.email).subscribe(
//      (exists: boolean) => {
//        this.emailExists = exists;
//      },
//      (err) => {
//        console.error("Email check failed:", err);
//        this.emailExists = false;
//      }
//    );
//  }

  phoneError: boolean = false;
  phoneTouched: boolean = false;

  validatePhoneNumber() {
    this.phoneTouched = true;
    this.phoneError = !this._service.validatePhoneNumber(this.user.phoneNumber);
  }


  registerUser(form: NgForm) {
    this._service.registerVendor(this.user).subscribe(
      (res) => {
        alert("Registered Successfully!")
        form.reset();
      },
      (err) => {
        alert("Registration Failed")
      },
      () => { console.log("Registration Executed Successfully"); }

    );
  }
}
