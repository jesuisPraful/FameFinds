import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common'; 

@Component({
  selector: 'app-user-commonlayout',
  templateUrl: './user-commonlayout.component.html',
  styleUrls: ['./user-commonlayout.component.css']
})
export class UserCommonlayoutComponent {
  constructor(private location: Location, private router: Router) { }

  goBack() {
    this.location.back();
  }

  logout() {
    // Example: Clear token and redirect
    localStorage.clear();
    this.router.navigate(['/login']);
  }

}
