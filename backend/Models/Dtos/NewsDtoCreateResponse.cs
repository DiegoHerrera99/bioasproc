namespace bioinsumos_asproc_backend.Models
{
    public class NewsDtoCreateResponse 
    {
        public News News { get; set; }
        public System.Net.HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}