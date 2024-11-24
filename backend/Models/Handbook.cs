using System;
using System.Collections.Generic;

namespace bioinsumos_asproc_backend.Models;

public partial class Handbook
{
    public uint HandbookId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Path { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Status { get; set; }
}
