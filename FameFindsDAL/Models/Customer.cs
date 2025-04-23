using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FameFindsDAL.Models;

public partial class Customer
{
    [Key]
    public int CustomerId { get; set; }
    [Required]
    [StringLength(100)]
    public string? FullName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 8)]
    public string? PasswordHash { get; set; }
    [Required]
    [Phone]   
    public string? PhoneNumber { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
