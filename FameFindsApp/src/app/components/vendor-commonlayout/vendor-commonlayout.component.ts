import { Component } from '@angular/core';
import { Location } from '@angular/common'
import { Router } from '@angular/router';

@Component({
  selector: 'app-vendor-commonlayout',
  templateUrl: './vendor-commonlayout.component.html',
  styleUrls: ['./vendor-commonlayout.component.css']
})
export class VendorCommonlayoutComponent {
  isSidebarOpen = false;

  constructor(private location: Location, private router: Router) { }

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

  goBack() {
    this.location.back();
  }

  logout() {
    // Perform logout logic, then redirect
    this.router.navigate(['/login']);
  }
}

