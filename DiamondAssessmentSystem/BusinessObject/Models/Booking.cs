using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public DateOnly BookingDate { get; set; }

    public int CustomerId { get; set; }

    public string Status { get; set; } = null!;

    public int Quantity { get; set; }

    public string? BookingDetailId { get; set; }

    public int? ConsultantId { get; set; }

    public int? ReceiptId { get; set; }

    public int? SealingId { get; set; }

    public int? CommitmentId { get; set; }

    public virtual Form? Commitment { get; set; }

    public virtual Staff? Consultant { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Form? Receipt { get; set; }

    public virtual Form? Sealing { get; set; }
}
