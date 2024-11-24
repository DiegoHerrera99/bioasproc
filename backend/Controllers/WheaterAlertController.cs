using System.Net;
using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WheaterAlertController : ControllerBase
    {
        private readonly IWheaterAlertService wheaterAlertService;
        private readonly List<string> validImageTypes = new() { "image/jpg", "image/jpeg", "image/png" };

        public WheaterAlertController(IWheaterAlertService wheaterAlertService)
        {
            this.wheaterAlertService = wheaterAlertService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWheaterAlert()
        {
            var list = await wheaterAlertService.GetWheaterAlerts();
            if (list == null) return StatusCode((int)HttpStatusCode.InternalServerError);

            var _list = list.Select(n => 
            {
                n.ImgPath = wheaterAlertService.GetWheaterAlertCDN(n.ImgPath);
                return n;
            }).ToArray();
            
            return Ok(_list);
        }

        [HttpGet("{wheaterAlertId}")]
        public async Task<IActionResult> GetWheaterAlertById(uint wheaterAlertId)
        {
            var wheaterAlert = await wheaterAlertService.GetWheaterAlertById(wheaterAlertId);
            if (wheaterAlert == null) return NotFound(new { Message = "wheaterAlertId not found." });

            wheaterAlert.ImgPath = wheaterAlertService.GetWheaterAlertCDN(wheaterAlert.ImgPath);
            return Ok(wheaterAlert);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostWheaterAlert(
            IFormFile file, 
            [FromForm] string title,
            [FromForm] string body,
            [FromForm] string url
        )
        {
            try
            {
                if (file == null || file.Length == 0) return BadRequest(new { Message = "file is required" });
                if (!validImageTypes.Contains(file.ContentType)) return BadRequest(new { Message = "file must be .jpg or .png" });
                if (title == null || title.Length == 0) return BadRequest(new { Message = "title is required" });
                if (body == null || body.Length == 0) return BadRequest(new { Message = "body is required" });

                WheaterAlert _wheaterAlert = new()
                {
                    Title = title,
                    Body = body,
                };

                if (url != null && url.Length >= 0) _wheaterAlert.Url = url;

                var res = await wheaterAlertService.CreateWheaterAlert(_wheaterAlert, file);
                if (res.Status == HttpStatusCode.InternalServerError) return StatusCode((int) res.Status, res);
                
                return CreatedAtAction(nameof(GetWheaterAlertById), new { wheaterAlertId = res.WheaterAlert.WeatherAlertId }, res.WheaterAlert);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateWheaterAlert(
            IFormFile file, 
            [FromForm] string wheaterAlertId, 
            [FromForm] string title, 
            [FromForm] string body,
            [FromForm] string url
        )
        {
            try
            {
                if (wheaterAlertId == null) return BadRequest(new { Message = "wheaterAlertId is required" });

                IFormFile _file = null;
                if (file != null && file.Length > 0) 
                {
                    _file = file; 
                    if (!validImageTypes.Contains(_file.ContentType)) return BadRequest(new { Message = "file must be .jpg or .png" });
                }

                uint _wheaterAlertId = uint.Parse(wheaterAlertId);
                var wheaterAlert = await wheaterAlertService.GetWheaterAlertById(_wheaterAlertId);
                if (wheaterAlert == null) return NotFound(new { Message = "wheaterAlertId not found." });

                var _wheaterAlert = new WheaterAlert();
                if (title != null && title.Length > 0) _wheaterAlert.Title = title;
                if (body != null && body.Length > 0) _wheaterAlert.Body = body;
                if (url != null) _wheaterAlert.Url = url;

                var res = await wheaterAlertService.UpdateWheaterAlert(_wheaterAlert, wheaterAlert, _file);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpDelete("{wheaterAlertId}")]
        [Authorize]
        public async Task<IActionResult> DeleteWheaterAlert(uint wheaterAlertId)
        {
            try
            {
                var wheaterAlert = await wheaterAlertService.GetWheaterAlertById(wheaterAlertId);
                if (wheaterAlert == null) return NotFound(new { Message = "wheaterAlertId not found." });

                var res = await wheaterAlertService.DeleteWheaterAlert(wheaterAlert);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
    }
}