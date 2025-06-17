export interface IRating {
  customerId: number;
  shopId: number;
  ratingValue: number;
  review: string;
  createdAt?: Date; // optional, only if needed
}
