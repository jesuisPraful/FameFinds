using System;
using System.Collections.Generic;

namespace FameFindsDAL.Models;

public partial class Rating
{
    public int RatingId { get; set; }

    public int? CustomerId { get; set; }

    public int? ShopId { get; set; }

    public int? RatingValue { get; set; }

    public string? Review { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Shop? Shop { get; set; }
}
