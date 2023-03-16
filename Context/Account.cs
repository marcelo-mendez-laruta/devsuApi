using System;
using System.Collections.Generic;

namespace devsuApi.Context;

public partial class Account
{
    public int AccountId { get; set; }

    public int Number { get; set; }

    public string Type { get; set; } = null!;

    public long Balance { get; set; }

    public bool State { get; set; }

    public int ClientId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<Movement> Movements { get; } = new List<Movement>();
}
