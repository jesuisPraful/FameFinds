using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FameFindsDAL.Models;

public partial class ShopProduct
{
    public int ShopProductId { get; set; }

    public int? ShopId { get; set; }

    public int? ProductId { get; set; }

    public decimal? Price { get; set; }

    public int? Stock { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }

    [ForeignKey("ShopId")]
    public virtual Shop? Shop { get; set; }
}
