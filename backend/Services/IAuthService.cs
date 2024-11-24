using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Models.Dtos;

namespace bioinsumos_asproc_backend.Services
{
    public interface IAuthService
    {   
        Task<AuthDtoResponse> GetToken(AuthDtoRequest authDtoRequest);
        Task<AuthDtoResponse> DeleteToken(string token);
        Task<User> GetUser(string Email, string Password);
        Task<SigninDtoResponse> SaveUser(AuthDtoRequest authDtoRequest);
    }
}