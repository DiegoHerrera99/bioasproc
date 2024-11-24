using System.Net;
using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HandbookController : ControllerBase
    {
        private readonly IHandbookService _handbookService;

        public HandbookController(IHandbookService handbookService)
        {
            _handbookService = handbookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _handbookService.GetHandbooks();
            if (list == null) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Ok(list);
        }

        [HttpGet("{handbookId}")]
        public async Task<IActionResult> GetHandbook(uint handbookId)
        {
            var handbook = await _handbookService.GetHandbookById(handbookId);
            if (handbook == null) return NotFound(new { Message = "handbookId not found." });

            var res = await _handbookService.DownloadHandbook(handbook.Path);
            if (res.Status == HttpStatusCode.NotFound) return NotFound(res);
            if (res.Status == HttpStatusCode.InternalServerError) return StatusCode((int) res.Status, res);

            return File(res.FileContent, "application/pdf", res.FileName);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Handbook>> PostHandbook(IFormFile file, [FromForm] string name, [FromForm] string description)
        {
            try
            {
                if (file == null || file.Length == 0) return BadRequest(new { Message = "file is required" });
                if (name == null || name.Length == 0) return BadRequest(new { Message = "name is required" });
                if (description == null || description.Length == 0) return BadRequest(new { Message = "description is required" });

                var res = await _handbookService.UploadHandbook(file, name, description);
                if (res.Status == HttpStatusCode.InternalServerError) return StatusCode((int) res.Status, res);
                
                return CreatedAtAction(nameof(GetHandbook), new { handbookId = res.Handbook.HandbookId }, res.Handbook);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> PutHandbook(IFormFile file, [FromForm] string handbookId, [FromForm] string name, [FromForm] string description)
        {
            try
            {
                IFormFile _file = null;
                if (file != null && file.Length > 0) _file = file; 

                uint _handbookId = uint.Parse(handbookId);
                var handbook = await _handbookService.GetHandbookById(_handbookId);
                if (handbook == null) return NotFound(new { Message = "handbookId not found." });

                var _handbook = new Handbook();
                if (name != null && name.Length > 0) handbook.Name = name;
                if (description != null && description.Length > 0) handbook.Description = description;

                var res = await _handbookService.UpdateHandbook(_handbook, handbook, _file);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpDelete("{handbookId}")]
        [Authorize]
        public async Task<ActionResult> Deletehandbook(uint handbookId)
        {
            try
            {
                var handbook = await _handbookService.GetHandbookById(handbookId);
                if (handbook == null) return NotFound(new { Message = "handbookId not found." });

                var res = await _handbookService.DeleteHandbook(handbook);
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