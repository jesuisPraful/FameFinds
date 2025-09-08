import { Time } from "@angular/common";

export interface IShop {
  shopId: number;
  shopName: string;
  emailId: string;
  cityId: number|null;
  pincode: string|null;
  contactNumber: string;
  fullAddress: string;
  latitude: number;
  longitude: number;
  isOpen: boolean;
  createdAt: Date;
  vendorId: number;
  openingTime?: string;
  closingTime?: string;

  averageRating?: number;
  totalRating?: number;

}
