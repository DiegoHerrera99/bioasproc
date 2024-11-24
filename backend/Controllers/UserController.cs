using Microsoft.AspNetCore.Mvc;

using bioinsumos_asproc_backend.Models.Dtos;
using bioinsumos_asproc_backend.Services;
using bioinsumos_asproc_backend.Resources;
using Microsoft.AspNetCore.Authorization;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthDtoRequest authDtoRequest)
        {
            authDtoRequest.Password = Utils.EncryptPassword(authDtoRequest.Password);
            var authResult = await _authService.GetToken(authDtoRequest);
            if (authResult == null) return Unauthorized();
            else return Ok(authResult);
        }

        [Authorize]
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var res = await _authService.DeleteToken(token);

            if (!res.Result) return BadRequest(res);
            return Ok(res);
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Signin([FromBody] AuthDtoRequest authDtoRequest)
        {
            authDtoRequest.Password = Utils.EncryptPassword(authDtoRequest.Password);
            var signinResult = await _authService.SaveUser(authDtoRequest);
            if (signinResult.Result == false) return BadRequest(signinResult);
            else return Created("", signinResult);
        }
    }
}