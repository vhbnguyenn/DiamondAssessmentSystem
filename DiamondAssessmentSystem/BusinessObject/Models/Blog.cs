using System;
using System.Collections.Generic;

namespace DiamondAssessmentSystem.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly BlogDate { get; set; }

    public string? Description { get; set; }

    public string Context { get; set; } = null!;

    public string? Image { get; set; }
}
