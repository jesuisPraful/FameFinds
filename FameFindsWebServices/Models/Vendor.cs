using System.ComponentModel.DataAnnotations;

namespace FameFindsWebServices.Models
{
    public class Vendor
    {
        [Required]
        public int VendorId { get; set; }
        [Required]
        public string? VendorName { get; set; }
        [Required]
        [StringLength(100)]
        public string? Email { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string? PasswordHash { get; set; }
        [Required]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }

    }
}
