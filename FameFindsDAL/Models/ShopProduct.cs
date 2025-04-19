using System;
using System.Collections.Generic;

namespace FameFindsDAL.Models;

public partial class ShopProduct
{
    public int ShopProductId { get; set; }

    public int? ShopId { get; set; }

    public int? ProductId { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Shop? Shop { get; set; }
}
