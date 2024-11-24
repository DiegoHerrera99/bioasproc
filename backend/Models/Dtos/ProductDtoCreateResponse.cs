namespace bioinsumos_asproc_backend.Models
{
    public class ProductDtoCreateResponse 
    {
        public Product Product { get; set; }
        public System.Net.HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}