using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Models;

public partial class Result
{
    public int ResultId { get; set; }

    public int AssessmentStaff { get; set; }

    public string? Origin { get; set; }

    public string? Shape { get; set; }

    public string? Measurement { get; set; }

    public string? CaratWeight { get; set; }

    public string? Color { get; set; }

    public string? Clarity { get; set; }

    public string? Cut { get; set; }

    public string? Proportion { get; set; }

    public string? Polish { get; set; }

    public string? Symmetry { get; set; }

    public string? Fluorescence { get; set; }

    public string? AssessmentNote { get; set; }

    public string? ManagerNote { get; set; }

    public bool? IsAccepted { get; set; }

    public int? CertId { get; set; }

    public virtual Staff AssessmentStaffNavigation { get; set; } = null!;

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Certificate? Cert { get; set; }
}
