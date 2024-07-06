using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Models;

public partial class Form
{
    public int FormId { get; set; }

    public string FormType { get; set; } = null!;

    public DateOnly CreateDate { get; set; }

    public virtual ICollection<Booking> BookingCommitments { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingReceipts { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingSealings { get; set; } = new List<Booking>();
}
