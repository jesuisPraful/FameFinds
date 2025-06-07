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
    user: IVendor = {
    vendorName: "",
    email: "",
    phoneNumber: "",
    passwordHash: ""
  };
  constructor(private _service: Register2Service ) { }

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
