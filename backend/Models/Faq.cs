using System;
using System.Collections.Generic;

namespace bioinsumos_asproc_backend.Models;

public partial class Faq
{
    public uint FaqId { get; set; }

    public string Question { get; set; }

    public string Answer { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Status { get; set; }
}
