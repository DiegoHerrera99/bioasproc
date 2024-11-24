using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public ActionResult SendEmail(SendEmailDto request)
        {
            var res = _emailService.SendEmail(request);
            if (!res) return BadRequest(new { message = "An error has occurred." });
            return Ok(new { message = "Mail sent!" });
        }
    }
}