namespace FameFindsWebServices.Models
{
    public class ShopDTO
    {
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string EmailId { get; set; }
        public int? CityId { get; set; }
        public string Pincode { get; set; }
        public string ContactNumber { get; set; }
        public string FullAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool? IsOpen { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int VendorId { get; set; }
    }
}
