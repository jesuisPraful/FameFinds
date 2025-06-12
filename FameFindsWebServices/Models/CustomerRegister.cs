using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FameFindsWebServices.Models
{
    public class CustomerRegister
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
