import { Component } from '@angular/core';
import { IVendor } from '../../Models/vendor';
import { Register2Service } from '../../services/register2.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-registration-page2',
  templateUrl: './registration-page2.component.html',
  styleUrls: ['./registration-page2.component.css']
})
export class RegistrationPage2Component {
  vendor: IVendor = {
    vendorId: "",
    vendorName: "",
    email: "",
    phoneNumber: "",
    passwordHash: ""
  };
  constructor(private _service: Register2Service ) { }

  registerVendor(form: NgForm) {
    this._service.registerVendor(this.vendor).subscribe(
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
