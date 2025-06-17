import { Injectable } from '@angular/core';
import { IRating } from '../Models/rating';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RatingService {
  ratings: IRating[] = []

  constructor(private _http: HttpClient) {

  }

  addRating(rating: IRating) {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this._http.post<string>(
      'https://localhost:7249/api/Rating',
      rating,
      { headers, responseType: 'text' as 'json' } 
    ).pipe(catchError(this.errorHandler));
  }
  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(() => error.message || "Server Error");
  }
}
