export interface IShop {
  shopId: number;
  shopName: string;
  emailId: string;
  cityId: number;
  pincode: string;
  contactNumber: string;
  fullAddress: string;
  latitude: number;
  longitude: number;
  isOpen: boolean;
  createdAt: Date;
  vendorId: number;
}
