namespace bioinsumos_asproc_backend.Models
{
    public class PdfDtoPostResponse
    {
        public Pdf Pdf1 { get; set; }
        public System.Net.HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}