import { Component, OnInit } from '@angular/core';
import { VendorService } from '../../services/vendor.service'; // ✅ Correct path

@Component({
  selector: 'app-view-rating',
  templateUrl: './view-rating.component.html',
  styleUrls: ['./view-rating.component.css']
})
export class ViewRatingComponent implements OnInit {
  ratings: any[] = [];

  constructor(private vendorService: VendorService) { }

  ngOnInit() {
    const vendorId = Number(localStorage.getItem('vendorId')); // ✅ Adjust as needed

    if (vendorId) {
      this.vendorService.getVendorRatings(vendorId).subscribe({
        next: (data: any[]) => this.ratings = data,
        error: (err: any) => console.error('Error fetching ratings', err)
      });
    } else {
      console.warn('Vendor ID not found in localStorage');
    }
  }

  getStars(score: number): number[] {
    return Array(score).fill(0);
  }
}
