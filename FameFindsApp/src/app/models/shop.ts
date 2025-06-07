export interface IShop {
  ShopId: number;
  ShopName: string;
  EmailId: string;
  CityId: number;
  Pincode: string;
  ContactNumber: string;
  FullAddress: string;
  Latitude: number;
  Longitude: number;
  OpeningTime: Date;
  ClosingTime: Date;
  IsOpen: boolean;
  CreatedAt: Date;
  VendorId: number;
         
}
