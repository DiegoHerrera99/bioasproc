using System.Net;
using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
/*         private readonly IReviewService _reviewService;
        private readonly ICourseService _courseService;
        
        public ReviewController(IReviewService reviewService, ICourseService courseService)
        {
            _reviewService = reviewService;
            _courseService = courseService;
        } */

/*         [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _reviewService.GetReviews();
            if (list == null) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Ok(list);
        } */

/*         [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetById(uint reviewId)
        {
            var review = await _reviewService.GetReviewById(reviewId);
            if (review == null) return NotFound();
            return Ok(review);
        } */

/*         [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(ReviewDtoPostRequest reviewDtoPostRequest)
        {
            var course = await _courseService.GetCourseById(reviewDtoPostRequest.CourseId);
            if (course == null) return NotFound();
            
            var res = await _reviewService.CreateReview(reviewDtoPostRequest);
            if (!res.Result) return BadRequest(res);
            return Created("", res);
        } */

/*         [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] ReviewDtoPutRequest reviewDtoPutRequest)
        {
            var review = await _reviewService.GetReviewById(reviewDtoPutRequest.ReviewId);
            if (review == null) return NotFound();

            var res = await _reviewService.UpdateReview(reviewDtoPutRequest, review);
            if (!res.Result) return BadRequest(res);

            return Ok(res);
        } */

/*         [HttpDelete("{reviewId}")]
        [Authorize]
        public async Task<IActionResult> Delete(uint reviewId)
        {
            var review = await _reviewService.GetReviewById(reviewId);
            if (review == null) return NotFound();

            var res = await _reviewService.DeleteReview(review);
            if (!res) return StatusCode((int)HttpStatusCode.InternalServerError);
            return NoContent();
        } */
    }
}