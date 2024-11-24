using System.Net;
using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService newsService;
        private readonly List<string> validImageTypes = new() { "image/jpg", "image/jpeg", "image/png" };

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            var list = await newsService.GetNews();
            if (list == null) return StatusCode((int)HttpStatusCode.InternalServerError);

            var _list = list.Select(n => 
            {
                n.ImgPath = newsService.GetNewsCDN(n.ImgPath);
                return n;
            }).ToArray();
            
            return Ok(_list);
        }

        [HttpGet("{newsId}")]
        public async Task<IActionResult> GetNewsById(uint newsId)
        {
            var news = await newsService.GetNewsById(newsId);
            if (news == null) return NotFound(new { Message = "newsId not found." });

            news.ImgPath = newsService.GetNewsCDN(news.ImgPath);
            return Ok(news);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostNews(
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

                News _news = new()
                {
                    Title = title,
                    Body = body,
                };

                if (url != null && url.Length >= 0) _news.Url = url;

                var res = await newsService.CreateNews(_news, file);
                if (res.Status == HttpStatusCode.InternalServerError) return StatusCode((int) res.Status, res);
                
                return CreatedAtAction(nameof(GetNewsById), new { newsId = res.News.NewId }, res.News);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateNews(
            IFormFile file, 
            [FromForm] string newsId, 
            [FromForm] string title, 
            [FromForm] string body,
            [FromForm] string url
        )
        {
            try
            {
                if (newsId == null) return BadRequest(new { Message = "newsId is required" });

                IFormFile _file = null;
                if (file != null && file.Length > 0) 
                {
                    _file = file; 
                    if (!validImageTypes.Contains(_file.ContentType)) return BadRequest(new { Message = "file must be .jpg or .png" });
                }

                uint _newsId = uint.Parse(newsId);
                var news = await newsService.GetNewsById(_newsId);
                if (news == null) return NotFound(new { Message = "newsId not found." });

                var _news = new News();
                if (title != null && title.Length > 0) _news.Title = title;
                if (body != null && body.Length > 0) _news.Body = body;
                if (url != null) _news.Url = url;

                var res = await newsService.UpdateNews(_news, news, _file);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpDelete("{newsId}")]
        [Authorize]
        public async Task<IActionResult> DeleteNews(uint newsId)
        {
            try
            {
                var news = await newsService.GetNewsById(newsId);
                if (news == null) return NotFound(new { Message = "newsId not found." });

                var res = await newsService.DeleteNews(news);
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