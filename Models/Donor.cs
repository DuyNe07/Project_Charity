using System;
using System.Collections.Generic;

namespace project_charity.Models;

public partial class Donor
{
    public int DonorId { get; set; }

    public string? Name { get; set; }

    public string? Number { get; set; }

    public string? Address { get; set; }

    public string? Bio { get; set; }

    public DateOnly? Birthday { get; set; }

    public byte[]? Avatar { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Donate> Donates { get; set; } = new List<Donate>();

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
