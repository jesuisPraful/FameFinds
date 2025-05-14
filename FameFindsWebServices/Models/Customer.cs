using System.ComponentModel.DataAnnotations;

namespace FameFindsWebServices.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        [MaxLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string? PasswordHash { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [MaxLength(15, ErrorMessage = "Phone Number cannot exceed 15 characters")]
        [Phone(ErrorMessage = "Invalid Phone Number format")]
        public string? PhoneNumber { get; set; }
    }
}
