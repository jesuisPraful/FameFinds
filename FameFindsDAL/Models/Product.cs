using System;
using System.Collections.Generic;

namespace FameFindsDAL.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? CityId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<ShopProduct> ShopProducts { get; set; } = new List<ShopProduct>();

    //public virtual ICollection<Product> Products { get; set; } = new List<Product>();
   
}
