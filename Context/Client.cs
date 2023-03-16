using System;
using System.Collections.Generic;

namespace devsuApi.Context;

public partial class Client
{
    public int ClientId { get; set; }

    public string Password { get; set; } = null!;

    public bool? State { get; set; }

    public int? PersonId { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();

    public virtual Person? Person { get; set; }
}
