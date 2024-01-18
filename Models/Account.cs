using System;
using System.Collections.Generic;

namespace project_charity.Models;

public partial class Account
{
    public string? Email { get; set; }

    public int AccountId { get; set; }

    public string? Password { get; set; }

    public int? Role { get; set; }

    public int? DonorId { get; set; }

    public virtual ICollection<Charity> Charities { get; set; } = new List<Charity>();

    public virtual Donor? Donor { get; set; }
}
