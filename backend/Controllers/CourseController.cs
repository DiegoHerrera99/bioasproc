using System.Net;
using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
           _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _courseService.GetCourses();
            if (list == null) return StatusCode((int)HttpStatusCode.InternalServerError);

            var _list = list.Select(c => 
            {
                if (c.ImgPath != null && c.ImgPath != "") c.ImgPath = _courseService.GetCourseCDN(c.ImgPath);
                return c;
            }).ToArray();
            
            return Ok(_list);
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetById(uint courseId)
        {
            var course = await _courseService.GetCourseById(courseId);
            if (course == null) return NotFound();

            if (course.ImgPath != null && course.ImgPath != "") course.ImgPath = _courseService.GetCourseCDN(course.ImgPath);
            return Ok(course);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post
        (
            IFormFile file, 
            [FromForm] string name,
            [FromForm] string description
        )
        {
            Course course = new()
            {
                Name = name,
                Description = description
            };
            var res = await _courseService.CreateCourse(course, file);
            if (res == null) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Created("", res);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put
        (
            IFormFile file, 
            [FromForm] string courseId,
            [FromForm] string name,
            [FromForm] string description
        )
        {
            uint _courseId = uint.Parse(courseId);
            var course = await _courseService.GetCourseById(_courseId);
            if (course == null) return NotFound();

            Course _course = new()
            {
                Name = name,
                Description = description
            };

            Course res;
            if (file != null && file.Length > 0) res = await _courseService.UpdateCourse(_course, course, file);
            else res = await _courseService.UpdateCourse(_course, course);

            if (res == null) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Ok(res);
        }

        [HttpDelete("{courseId}")]
        [Authorize]
        public async Task<IActionResult> Delete(uint courseId)
        {
            var course = await _courseService.GetCourseById(courseId);
            if (course == null) return NotFound();

            var res = await _courseService.DeleteCourse(course);
            if (!res) return StatusCode((int)HttpStatusCode.InternalServerError);
            return NoContent();
        }
    }
}