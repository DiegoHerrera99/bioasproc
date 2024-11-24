namespace bioinsumos_asproc_backend.Models
{
    public class VideoDtoStreamResponse
    {
        public System.Net.HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public string CdnUrl { get; set; }
    }
}