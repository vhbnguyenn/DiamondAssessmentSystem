using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Models;

public partial class ServicePrice
{
    public int ServicePriceId { get; set; }

    public string ServiceType { get; set; } = null!;

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public string? Duration { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
}
