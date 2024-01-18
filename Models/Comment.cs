using System;
using System.Collections.Generic;

namespace project_charity.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public double? Value { get; set; }

    public int? DonorId { get; set; }

    public int? CampainId { get; set; }

    public virtual Campaign? Campain { get; set; }

    public virtual Donor? Donor { get; set; }
}
