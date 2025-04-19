using System;
using System.Collections.Generic;

namespace FameFindsDAL.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
