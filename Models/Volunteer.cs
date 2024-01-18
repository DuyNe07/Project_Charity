using System;
using System.Collections.Generic;

namespace project_charity.Models;

public partial class Volunteer
{
    public int VolunteerId { get; set; }

    public string? Job { get; set; }

    public string? Education { get; set; }

    public string? Experience { get; set; }

    public int? DonorId { get; set; }

    public virtual Donor? Donor { get; set; }

    public virtual ICollection<Campaign> Campains { get; set; } = new List<Campaign>();
}
