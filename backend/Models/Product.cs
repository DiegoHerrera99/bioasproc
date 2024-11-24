using System;
using System.Collections.Generic;

namespace bioinsumos_asproc_backend.Models;

public partial class Product
{
    public uint ProductId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImgPath { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Status { get; set; }
}
