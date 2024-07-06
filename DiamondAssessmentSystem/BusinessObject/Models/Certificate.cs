using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Models;

public partial class Certificate
{
    public int CertId { get; set; }

    public DateOnly IssueDate { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
