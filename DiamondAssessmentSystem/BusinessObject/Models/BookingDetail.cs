using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Models;

public partial class BookingDetail
{
    public int BookingDetailId { get; set; }

    public int ServicePriceId { get; set; }

    public int? ResultId { get; set; }

    public bool? IsAccepted { get; set; }

    public virtual Result? Result { get; set; }

    public virtual ServicePrice ServicePrice { get; set; } = null!;
}
