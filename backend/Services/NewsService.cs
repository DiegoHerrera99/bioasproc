using System.Net;
using bioinsumos_asproc_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace bioinsumos_asproc_backend.Services
{
    public class NewsService : INewsService
    {
        private readonly string S3BucketNewsFolder;
        private readonly BioinsumosContext _dbcontext;
        private readonly IAWSImageService _AWSImageService;

        public NewsService(IConfiguration config, BioinsumosContext dbcontext, IAWSImageService aWSImageService)
        {
            S3BucketNewsFolder = config.GetValue<string>("AWS:S3BucketNewsFolder");
            _dbcontext = dbcontext;
            _AWSImageService = aWSImageService;
        }

        public async Task<NewsDtoCreateResponse> CreateNews(News news, IFormFile file)
        {
            NewsDtoCreateResponse res = new ();
            try
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    bool uploadToS3res = await _AWSImageService.UploadImageToS3Async(fileName, S3BucketNewsFolder, file.ContentType, stream);
                }

                var _news = new News
                {
                    Title = news.Title,
                    Body = news.Body,
                    ImgPath = fileName
                };

                if (news.Url != null) _news.Url = news.Url;

                _dbcontext.News.Add(_news);
                await _dbcontext.SaveChangesAsync();

                res.Status = HttpStatusCode.OK;
                res.Message = "OK";
                res.News = _news;
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = ex.InnerException.Message;
                return res;
            }
        }

        public async Task<NewsDtoDeleteResponse> DeleteNews(News news)
        {
            NewsDtoDeleteResponse res = new();
            try
            {
                var deleteRes = await _AWSImageService.DeleteImageFromS3Async(news.ImgPath, S3BucketNewsFolder);
                if (!deleteRes)
                {
                    res.Status = HttpStatusCode.InternalServerError;
                    res.Message = "An error occurred while deleting the news.";
                    return res;
                }

                _dbcontext.News.Remove(news);
                await _dbcontext.SaveChangesAsync();
                res.Status = HttpStatusCode.OK;
                res.Message = "Ok";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while deleting the news: {ex.Message}";
                return res;
            }
        }

        public async Task<List<News>> GetNews()
        {
            try
            {
                return await _dbcontext.News
                    .Where(n => n.Status == true)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return await Task.FromException<List<News>>(null);
            }
        }

        public string GetNewsCDN(string path)
        {
            return _AWSImageService.DownloadImageFromS3Async(path, S3BucketNewsFolder);
        }

        public async Task<News> GetNewsById(uint newsId)
        {
            try 
            {
                return await _dbcontext.News
                    .Where(n =>
                        n.NewId == newsId &&
                        n.Status == true
                    )
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<News>(null);
            }
        }

        public async Task<NewsDtoUpdateResponse> UpdateNews(News _news, News news, IFormFile file = null)
        {
            NewsDtoUpdateResponse res = new();
            try
            {
                if (file != null) {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        stream.Position = 0;
                        bool uploadToS3res = await _AWSImageService.UploadImageToS3Async(news.ImgPath, S3BucketNewsFolder, file.ContentType, stream);
                    }
                }

                if (_news.Title != null && _news.Title != news.Title) news.Title = _news.Title;
                if (_news.Body != null && _news.Body != news.Body) news.Body = _news.Body;
                if (_news.Url != null && _news.Url != news.Url) news.Url = _news.Url;
                await _dbcontext.SaveChangesAsync();

                res.Status = HttpStatusCode.OK;
                res.Message = "News updated successfully.";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while updating news: {ex.Message}";
                return res;
            }
        }
    }
}