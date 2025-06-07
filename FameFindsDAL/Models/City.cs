using System;
using System.Collections.Generic;

namespace FameFindsDAL.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
}
