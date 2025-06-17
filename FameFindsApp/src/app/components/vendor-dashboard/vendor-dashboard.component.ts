import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-vendor-dashboard',
  templateUrl: './vendor-dashboard.component.html',
  styleUrls: ['./vendor-dashboard.component.css']
})
export class VendorDashboardComponent {
  constructor(private router: Router) { }

  navigateTo(path: string): void {
    this.router.navigate([`/${path}`]);
  }
}
