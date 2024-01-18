using System;
using System.Collections.Generic;

namespace project_charity.Models;

public partial class Donate
{
    public double? Value { get; set; }

    public string? PaymentMethod { get; set; }

    public DateOnly? Date { get; set; }

    public int DonorId { get; set; }

    public int CampainId { get; set; }

    public virtual Campaign Campain { get; set; } = null!;

    public virtual Donor Donor { get; set; } = null!;
}
