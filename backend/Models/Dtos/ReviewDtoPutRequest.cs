using System;
using System.Collections.Generic;

namespace bioinsumos_asproc_backend.Models;

public partial class ReviewDtoPutRequest
{
    public uint ReviewId { get; set; }
    public string Response { get; set; }
}