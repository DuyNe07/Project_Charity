using System;
using System.Collections.Generic;

namespace project_charity.Models;

public partial class Campaign
{
    public int CampainId { get; set; }

    public string? Name { get; set; }

    public DateOnly? DateBegin { get; set; }

    public DateOnly? DateEnd { get; set; }

    public double? Goals { get; set; }

    public string? Describe { get; set; }

    public string? TagLine { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Donate> Donates { get; set; } = new List<Donate>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Charity> Charities { get; set; } = new List<Charity>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
}
