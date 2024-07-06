using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public int? AccId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual Account? Acc { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
