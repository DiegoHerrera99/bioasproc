using System;
using System.Collections.Generic;

namespace bioinsumos_asproc_backend.Models;

public partial class User
{
    public uint UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? ModifiedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Status { get; set; }
}
