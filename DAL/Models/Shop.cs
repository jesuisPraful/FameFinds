using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Shop
{
    public int ShopId { get; set; }

    public string? ShopName { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public int? VendorId { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<ShopProduct> ShopProducts { get; set; } = new List<ShopProduct>();

    public virtual Vendor? Vendor { get; set; }
}
