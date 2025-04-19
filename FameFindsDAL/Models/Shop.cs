using System;
using System.Collections.Generic;

namespace FameFindsDAL.Models;

public partial class Shop
{
    public int ShopId { get; set; }

    public string? ShopName { get; set; }

    public string EmailId { get; set; } = null!;

    public string CityName { get; set; } = null!;

    public string Pincode { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public string FullAddress { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public TimeOnly? OpeningTime { get; set; }

    public TimeOnly? ClosingTime { get; set; }

    public bool? IsOpen { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int VendorId { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<ShopProduct> ShopProducts { get; set; } = new List<ShopProduct>();

    public virtual Vendor Vendor { get; set; } = null!;
}
