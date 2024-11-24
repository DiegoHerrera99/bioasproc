using System.Net;
using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly IPdfService _pdfService;
        private readonly ICourseService _courseService;

        public PdfController(IPdfService pdfService, ICourseService courseService)
        {
            _pdfService = pdfService;
            _courseService = courseService;
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetPdf(uint courseId)
        {
            var course = await _courseService.GetCourseById(courseId);
            if (course == null) return NotFound(new { Message = "courseId not found." });

            var pdf = course.Pdfs.FirstOrDefault();
            if (pdf == null) return NotFound(new { Message = "course without pdf file." });

            var res = await _pdfService.FetchFile(pdf.Path);
            if (res.Status == HttpStatusCode.NotFound) return NotFound(res);
            if (res.Status == HttpStatusCode.InternalServerError) return StatusCode((int) res.Status, res);

            return File(res.FileContent, "application/pdf", res.FileName);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Pdf>> PostPdf(IFormFile file, [FromForm] string courseId)
        {
            try
            {
                if (file == null || file.Length == 0) return BadRequest(new { Message = "File not provided" });

                uint _courseId = uint.Parse(courseId);
                Course course = await _courseService.GetCourseById(_courseId);
                if (course == null) return NotFound(new { Message = "courseId not found." });
                if (course.Pdfs.FirstOrDefault() != null) return BadRequest(new { Message = "Pdf associated to course already exists." });

                var res = await _pdfService.UploadFile(file, _courseId);
                if (res.Status == HttpStatusCode.InternalServerError) return StatusCode((int) res.Status, res);
                
                return CreatedAtAction(nameof(GetPdf), new { courseId = res.Pdf1.CourseId }, res.Pdf1);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> PutPdf(IFormFile file, [FromForm] string courseId)
        {
            try
            {
                if (file == null || file.Length == 0) return BadRequest(new { Message = "File not provided" });

                uint _courseId = uint.Parse(courseId);
                var course = await _courseService.GetCourseById(_courseId);
                if (course == null) return NotFound(new { Message = "courseId not found." });

                var pdf = course.Pdfs.FirstOrDefault();
                if (pdf == null) return NotFound(new { Message = "course without pdf file." });

                var res = await _pdfService.UpdateFile(file, pdf);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpDelete("{courseId}")]
        [Authorize]
        public async Task<ActionResult> DeletePdf(uint courseId)
        {
            try
            {
                var course = await _courseService.GetCourseById(courseId);
                if (course == null) return NotFound(new { Message = "courseId not found." });
                var pdf = course.Pdfs.FirstOrDefault();
                if (pdf == null) return NotFound(new { Message = "course without pdf file." });
                var res = await _pdfService.DeleteFile(pdf);
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