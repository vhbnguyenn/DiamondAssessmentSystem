using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int? AccId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string Phone { get; set; } = null!;

    public string? IdCard { get; set; }

    public string Address { get; set; } = null!;

    public string? UnitName { get; set; }

    public string? TaxCode { get; set; }

    public virtual Account? Acc { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
