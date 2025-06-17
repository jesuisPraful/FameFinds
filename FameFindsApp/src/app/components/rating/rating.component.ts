import { Component, OnInit } from '@angular/core';
import { RatingService } from '../../services/rating.service';
import { IRating } from '../../Models/rating';
import { trigger, transition, style, animate } from '@angular/animations';
import { Router } from '@angular/router';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css'],
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(30px)' }),
        animate('600ms ease-out', style({ opacity: 1, transform: 'translateY(0)' })),
      ])
    ])
  ]
})
export class RatingComponent implements OnInit {
  rating = 0;
  hoverRating = 0;
  stars = new Array(5);
  feedback = '';
  customerId: number | null = null;
  shopId: number | null = null;

  showSuccess = false;
  showError = false;
  errorMessage = '';

  constructor(private ratingService: RatingService, private router: Router) { }

  ngOnInit(): void {
    const storedCustomerId = localStorage.getItem('customerId');
    const storedShopId = localStorage.getItem('shopId'); // fixed key

    if (storedCustomerId && storedShopId) {
      this.customerId = parseInt(storedCustomerId, 10);
      this.shopId = parseInt(storedShopId, 10);
    } else {
      // No need to show error to user
      this.router.navigate(['/view-shops']);
    }
  }

  setRating(value: number) {
    this.rating = value;
  }

  submitFeedback() {
    if (!this.rating || !this.customerId || !this.shopId) {
      this.showError = true;
      this.errorMessage = 'Please provide a rating before submitting.';
      return;
    }

    const payload: IRating = {
      customerId: this.customerId,
      shopId: this.shopId,
      ratingValue: this.rating,
      review: this.feedback,
      createdAt: new Date()
    };

    this.ratingService.addRating(payload).subscribe({
      next: () => {
        this.showSuccess = true;
        this.showError = false;

        setTimeout(() => {
          this.router.navigate(['/view-shops']);
        }, 1500);
      },
      error: (err) => {
        this.showError = true;
        this.errorMessage = 'Failed to submit rating. Please try again.';
        console.error(err);
      }
    });
  }
}
