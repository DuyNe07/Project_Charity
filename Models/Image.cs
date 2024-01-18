using System;
using System.Collections.Generic;

namespace project_charity.Models;

public partial class Image
{
    public int ImageId { get; set; }

    public byte[]? ImageData { get; set; }

    public int? CampainId { get; set; }

    public virtual Campaign? Campain { get; set; }
}
