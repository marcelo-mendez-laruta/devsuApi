using System;
using System.Collections.Generic;

namespace devsuApi.Context;

public partial class Movement
{
    public int MovementId { get; set; }

    public DateTime Date { get; set; }

    public string? Type { get; set; }

    public long Amount { get; set; }

    public long Balance { get; set; }

    public string? Description { get; set; }

    public int? AccountId { get; set; }

    public virtual Account? Account { get; set; }
}
