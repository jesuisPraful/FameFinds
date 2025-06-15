export interface IShop {
  shopId: number;
  shopName: string;
  emailId: string;
  cityId: number|null;
  pincode: string|null;
  contactNumber: string;
  fullAddress: string;
  latitude: number|null;  
  longitude: number|null;
  openingTime: Date;
  closingTime: Date;
  isOpen: boolean;
  createdAt: Date;
  vendorId: number;
}
