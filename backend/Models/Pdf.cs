using System;
using System.Collections.Generic;

namespace bioinsumos_asproc_backend.Models;

public partial class Pdf
{
    public uint PdfId { get; set; }

    public uint? CourseId { get; set; }

    public string Path { get; set; } = null!;

    public DateTime? ModifiedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Status { get; set; }

    //public virtual Course? Course { get; set; }
}
