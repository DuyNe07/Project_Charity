using System;
using System.Collections.Generic;

namespace project_charity.Models;

public partial class Charity
{
    public int CharitiesId { get; set; }

    public string? Name { get; set; }

    public string? Number { get; set; }

    public string? Address { get; set; }

    public string? Bio { get; set; }

    public DateOnly? Birthday { get; set; }

    public string? Experience { get; set; }

    public byte[]? Avatar { get; set; }

    public int? AccountId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Campaign> Campains { get; set; } = new List<Campaign>();
}
