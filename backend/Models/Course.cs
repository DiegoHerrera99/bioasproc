using System;
using System.Collections.Generic;

namespace bioinsumos_asproc_backend.Models;

public partial class Course
{
    public uint CourseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Status { get; set; }

    public string ImgPath { get; set; }

    public virtual ICollection<Pdf> Pdfs { get; } = new List<Pdf>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual ICollection<Video> Videos { get; } = new List<Video>();
}
