import { Component, OnInit } from '@angular/core';
import { VendorService } from '../../services/vendor.service';

@Component({
  selector: 'app-view-rating',
  templateUrl: './view-rating.component.html',
  styleUrls: ['./view-rating.component.css']
})
export class ViewRatingComponent implements OnInit {
  ratings: any[] = [];

  constructor(private vendorService: VendorService) { }

  ngOnInit(): void {
    this.vendorService.getVendorRatings().subscribe({
      next: (data) => this.ratings = data,
      error: (err) => console.error('Error fetching vendor ratings', err)
    });
  }

  getStars(score: number): number[] {
    return Array(score).fill(0);
  }
}
