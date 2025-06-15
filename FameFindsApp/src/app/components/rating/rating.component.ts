import { Component } from '@angular/core';
import { trigger, transition, style, animate } from '@angular/animations';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css'],
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(30px)' }),
        animate(
          '600ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' })
        ),
      ]),
    ]),
  ],
})
export class RatingComponent {
  rating = 0;
  hoverRating = 0;
  stars = new Array(5);
  feedback = '';

  customerId = 1; // 🔁 Replace with actual logged-in user ID
  shopId = 101;   // 🔁 Replace with actual shop ID being rated

  constructor(private http: HttpClient) { }

  setRating(value: number) {
    this.rating = value;
  }

  submitFeedback() {
    if (!this.rating) {
      alert('Please select a star rating.');
      return;
    }

    const payload = {
      customerId: this.customerId,
      shopId: this.shopId,
      ratingValue: this.rating,
      review: this.feedback
    };

    this.http.post('https://localhost:7249/api/Rating', payload).subscribe({
      next: (res) => {
        alert('Thank you for your feedback!');
        this.rating = 0;
        this.hoverRating = 0;
        this.feedback = '';
      },
      error: (err) => {
        alert('Error submitting rating. Please try again.');
        console.error(err);
      }
    });
  }
}
