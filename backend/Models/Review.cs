using System;
using System.Collections.Generic;

namespace bioinsumos_asproc_backend.Models;

public partial class Review
{
    public uint ReviewId { get; set; }

    public uint? CourseId { get; set; }

    public string? Review1 { get; set; }

    public decimal? Score { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }
    public string? Response { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Status { get; set; }
}