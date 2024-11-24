namespace bioinsumos_asproc_backend.Models
{
    public class HandbookDtoUploadResponse
    {
        public Handbook Handbook { get; set; }
        public System.Net.HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}