using System;
using System.Collections.Generic;

namespace bioinsumos_asproc_backend.Models;

public partial class ReviewDtoPostRequest
{
    public uint CourseId { get; set; }

    public string Review { get; set; }

    public decimal Score { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }

}