using System.Net;
using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;
        private readonly ICourseService _courseService;

        public VideoController(IVideoService videoService, ICourseService courseService)
        {
            _videoService = videoService;
            _courseService = courseService;
        }

        [HttpGet("stream/{courseId}")]
        public async Task<IActionResult> GetStreamVideo(uint courseId)
        {
            try
            {
                var course = await _courseService.GetCourseById(courseId);
                if (course == null) return NotFound(new { Message = "courseId not found." });

                var video = course.Videos.FirstOrDefault();
                if (video == null) return BadRequest(new { Message = "course without video." });

                var res = _videoService.StreamVideo(video.Path);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { Message = $"An error occurred while trying to stream the video: {ex.Message}" });
            }
        }

        [HttpGet("download/{courseId}")]
        public async Task<IActionResult> GetDownloadVideo(uint courseId)
        {
            try
            {
                var course = await _courseService.GetCourseById(courseId);
                if (course == null) return NotFound(new { Message = "courseId not found." });

                var video = course.Videos.FirstOrDefault();
                if (video == null) return NotFound(new { Message = "course without video." });

                var res = await _videoService.DownloadVideo(video.Path);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);

                return File(res.FileContent, "application/mp4", res.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { Message = $"An error occurred while downloading the video: {ex.Message}" });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostVideo(IFormFile videoFile, [FromForm] string courseId)
        {
            try
            {
                uint _courseId = uint.Parse(courseId);
                Course course = await _courseService.GetCourseById(_courseId);
                if (course == null) return NotFound(new { Message = "courseId not found." });
                if (course.Videos.FirstOrDefault() != null) return BadRequest(new { Message = "course already has a video." });

                var res = await _videoService.UploadVideo(videoFile, _courseId);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { Message = $"An error occurred while posting the video: {ex.Message}" });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateVideo(IFormFile videoFile, [FromForm] string courseId)
        {
            try
            {
                if (videoFile == null || videoFile.Length == 0) return BadRequest(new { Message = "Video not provided." });

                uint _courseId = uint.Parse(courseId);
                var course = await _courseService.GetCourseById(_courseId);
                if (course == null) return NotFound(new { Message = "courseId not found." });

                var video = course.Videos.FirstOrDefault();
                if (video == null) return NotFound(new { Message = "course without video." });

                var res = await _videoService.UpdateVideo(videoFile, video);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { Message = $"An error occurred while updating the video: {ex.Message}" });
            }
        }

        [HttpDelete("{courseId}")]
        [Authorize]
        public async Task<IActionResult> DeleteVideo(uint courseId)
        {
            try
            {
                var course = await _courseService.GetCourseById(courseId);
                if (course == null) return NotFound(new { Message = "courseId not found." });

                var video = course.Videos.FirstOrDefault();
                if (video == null) return NotFound(new { Message = "course without video." });

                var res = await _videoService.DeleteVideo(video);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { Message = $"An error occurred while deleting the video: {ex.Message}" });
            }
        }
    }
}