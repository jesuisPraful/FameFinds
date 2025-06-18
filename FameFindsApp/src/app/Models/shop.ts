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
  averageRating?: number;
  totalRating?: number;
}
