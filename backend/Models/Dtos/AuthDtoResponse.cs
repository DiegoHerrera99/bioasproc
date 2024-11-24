namespace bioinsumos_asproc_backend.Models.Dtos
{
    public class AuthDtoResponse
    {
        public string Token { get; set; }

        public bool Result { get; set;}

        public string Message { get; set; }
    }
}