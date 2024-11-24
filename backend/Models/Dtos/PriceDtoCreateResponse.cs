namespace bioinsumos_asproc_backend.Models
{
    public class PriceDtoCreateResponse 
    {
        public Price Price { get; set; }
        public System.Net.HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}