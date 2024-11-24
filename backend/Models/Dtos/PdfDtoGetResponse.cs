namespace bioinsumos_asproc_backend.Models
{
    public class PdfDtoGetResponse
    {
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }
        public System.Net.HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}