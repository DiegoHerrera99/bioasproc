using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public interface IEmailService
    {
        bool SendEmail(SendEmailDto request);
    }
}