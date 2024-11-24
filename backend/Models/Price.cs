using System;
using System.Collections.Generic;

namespace bioinsumos_asproc_backend.Models;

public partial class Price
{
    public uint PriceId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public ushort? Price1 { get; set; }

    public string ImgPath { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Status { get; set; }
}
