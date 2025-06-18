import { Component, OnInit } from '@angular/core';
import { VendorService } from '../../services/vendor.service'; // ✅ Correct path
import { RatingService } from '../../services/rating.service';

@Component({
  selector: 'app-view-rating',
  templateUrl: './view-rating.component.html',
  styleUrls: ['./view-rating.component.css']
})
export class ViewRatingComponent implements OnInit {
  averageRating: number = 0;
  totalRatings: number = 0;
  shopId: number = 0;

  // ✅ Expose these properly
  Math = Math;
  Number = Number;

  constructor(private ratingService: RatingService) { }

  ngOnInit() {
    this.shopId = Number(localStorage.getItem('shopId'));
    this.ratingService.getAverageRating(this.shopId).subscribe({
      next: (response) => {
        this.averageRating = response.averageRating;
        this.totalRatings = response.totalRatings;
      },
      error: (err) => {
        console.error('Error fetching average rating:', err);
      }
    });
  }
}
