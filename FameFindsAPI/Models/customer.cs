using System.ComponentModel.DataAnnotations;

namespace FameFindsAPI.Models
{
    public class customer
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PasswordHash { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }

    }
}
