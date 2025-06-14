using System;
using System.Collections.Generic;

namespace FameFindsDAL.Models;

public partial class CustomerPasswordResetToken
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Expiry { get; set; }

    public bool? IsUsed { get; set; }

    public DateTime? RequestedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
