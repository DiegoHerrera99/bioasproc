using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController: ControllerBase 
    {
        //[Authorize] 
        /*[HttpGet]
        public async Task<IActionResult> ListCountries() 
        {
            var countries = await Task.FromResult(new List<string> { "France", "Argentina", "Croatia", "Morocco" });
            return Ok(countries);
        }*/
    }
}
