using System;
using System.Collections.Generic;

namespace bioinsumos_asproc_backend.Models;

public partial class CertifionInformation
{
    public uint CertificationInformationId { get; set; }

    public string Title { get; set; }

    public string Body { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? Status { get; set; }
}
