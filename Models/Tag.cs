using System;
using System.Collections.Generic;

namespace project_charity.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Campaign> Campains { get; set; } = new List<Campaign>();
}
