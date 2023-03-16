using System;
using System.Collections.Generic;

namespace devsuApi.Context;

public partial class Person
{
    public int PersonId { get; set; }

    public string Name { get; set; } = null!;

    public string? Gender { get; set; }

    public int? Age { get; set; }

    public string? Identification { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual Client? Client { get; set; }
}
