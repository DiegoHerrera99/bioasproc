namespace bioinsumos_asproc_backend.Models
{
    public class WheaterAlertDtoCreateResponse 
    {
        public WheaterAlert WheaterAlert { get; set; }
        public System.Net.HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}