namespace bioinsumos_asproc_backend.Models.Dtos
{
    public class SigninDtoResponse
    {
        public uint UserId { get; set; }

        public bool Result { get; set;}

        public string Message { get; set; }
    }
}