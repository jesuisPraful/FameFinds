using System;
using System.Collections.Generic;

namespace FameFindsDAL.Models;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string? VendorName { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
}
