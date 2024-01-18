using System;
using System.Collections.Generic;

namespace project_charity.Models;

public partial class PaymentMethod
{
    public int PaymentMethodId { get; set; }

    public string? Providder { get; set; }

    public string? AccountNumber { get; set; }

    public string? Name { get; set; }

    public int? DonorId { get; set; }

    public virtual Donor? Donor { get; set; }
}
