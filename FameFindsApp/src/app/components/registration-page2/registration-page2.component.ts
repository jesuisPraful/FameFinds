import { Component } from '@angular/core';
import { IVendor } from '../../Models/vendor';
import { Register2Service } from '../../services/register2.service';
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
      passwordHash: "",
     
  };
  emailExists: boolean = false;
  constructor(private _service: Register2Service, private _router: Router) { }
  

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
//import { Component } from '@angular/core';

////@Component({
////  selector: 'app-registration-page2',
////  templateUrl: './registration-page2.component.html',
////  styleUrls: ['./registration-page2.component.css']
////})
////export class RegistrationPage2Component {
////  vendor: IVendor = {
////    vendorId: "",
////    vendorName: "",
////    email: "",
////    phoneNumber: "",
////    passwordHash: ""
////  };
////  emailExists: boolean = false;
////  constructor(private _service: Register2Service) { }
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


    validatePhoneNumber() {
      this.phoneTouched = true;
      this.phoneError = !this._service.validatePhoneNumber(this.vendor.phoneNumber);
    }
    registerVendor(form: NgForm) {
      this._service.registerVendor(this.vendor).subscribe(
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
