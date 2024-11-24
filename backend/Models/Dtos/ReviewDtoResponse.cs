namespace bioinsumos_asproc_backend.Models;

public partial class ReviewDtoResponse
{
    public bool Result { get; set; }
    public string Message { get; set; }
    public Review Review1 { get; set; } = null;
}