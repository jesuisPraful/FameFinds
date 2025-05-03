using System.ComponentModel.DataAnnotations;

namespace FameFindsWebServices.Models
{
    public class Shop
    {
        [Key]
        public int ShopId { get; set; }

        [Required]
        public string? ShopName { get; set; }

        [Required]
        public string EmailId { get; set; } = null!;

        [Required]
        public string CityName { get; set; } = null!;

        [Required]
        public string Pincode { get; set; } = null!;

        [Required]
        public string ContactNumber { get; set; } = null!;

        [Required]
        public string FullAddress { get; set; } = null!;

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        public TimeOnly? OpeningTime { get; set; }

        public TimeOnly? ClosingTime { get; set; }

        public bool? IsOpen { get; set; }

        public DateTime? CreatedAt { get; set; }

        [Required]
        public int VendorId { get; set; }
    }
}
