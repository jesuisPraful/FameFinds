import { Component } from '@angular/core';
import { RegisterService } from '../../services/register.service';
import { ICustomer } from '../../Models/customer';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.css']
})
export class RegistrationPageComponent {
  user: ICustomer = {
    fullname: "",
    email: "",
    phoneNumber: "",
    password: ""
  };
  constructor(private _service: RegisterService) { }

  registerUser(form: NgForm) {
    this._service.registerCustomer(this.user).subscribe(
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
