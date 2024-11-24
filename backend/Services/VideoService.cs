using System.Net;
using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public class VideoService : IVideoService
    {
        private readonly BioinsumosContext _dbcontext;
        private readonly IAWSService _AWSVideoService;

        public VideoService(BioinsumosContext dbcontext, IServiceProvider serviceProvider)
        {
            _dbcontext = dbcontext;
           _AWSVideoService = serviceProvider
                .GetServices<IAWSService>()
                .OfType<AWSVideoService>()
                .FirstOrDefault();
        }

        public async Task<VideoDtoDeleteResponse> DeleteVideo(Video video)
        {
            VideoDtoDeleteResponse res = new();
            try
            {
                var deleteRes = await _AWSVideoService.DeleteFileFromS3Async(video.Path);
                if (!deleteRes)
                {
                    res.Status = HttpStatusCode.InternalServerError;
                    res.Message = "An error occurred while deleting the video.";
                    return res;
                }

                _dbcontext.Videos.Remove(video);
                await _dbcontext.SaveChangesAsync();
                res.Status = HttpStatusCode.OK;
                res.Message = "Ok, video deleted.";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while deleting the video: {ex.Message}";
                return res;
            }
        }

        public async Task<VideoDtoGetResponse> DownloadVideo(string path)
        {
            VideoDtoGetResponse res = new();
            try
            {
                res.FileContent = await _AWSVideoService.DownloadFileFromS3Async(path);
                res.FileName = path;
                res.Message = "Ok, video file fetched.";
                res.Status = HttpStatusCode.OK;
                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Status = HttpStatusCode.InternalServerError;
                return res;
            }
        }

        public VideoDtoStreamResponse StreamVideo(string path)
        {
            VideoDtoStreamResponse res = new();
            try
            {
                res.CdnUrl = _AWSVideoService.GetVideoStreamingFromS3Async(path);
                res.Status = HttpStatusCode.OK;
                res.Message = "Success with stream video.";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while streaming the video: {ex.Message}";
                return res;
            }
        }

        public async Task<VideoDtoPutResponse> UpdateVideo(IFormFile file, Video video)
        {
            VideoDtoPutResponse res = new();
            try
            {
                if (file.ContentType != "video/mp4")
                {
                    res.Status = HttpStatusCode.BadRequest;
                    res.Message = "The file must be an MP4 video.";
                    return res;
                }


                //SUBE EL ARCHIVO
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    ms.Position = 0;
                    bool uploadRes = await _AWSVideoService.UploadFileToS3Async(video.Path, ms);
                }

                //Forzar actualización de modifiedAt
                video.Status = false;
                await _dbcontext.SaveChangesAsync();
                video.Status = true;
                await _dbcontext.SaveChangesAsync();

                res.Status = HttpStatusCode.OK;
                res.Message = "Video updated successfully.";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while updating the video: {ex.Message}";
                return res;
            }
        }

        public async Task<VideoDtoPostResponse> UploadVideo(IFormFile file, uint courseId)
        {
            VideoDtoPostResponse res = new();
            try 
            {
                if (file.ContentType != "video/mp4")
                {
                    res.Status = HttpStatusCode.BadRequest;
                    res.Message = "The file must be an MP4 video.";
                    return res;
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                //SUBE EL ARCHIVO
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    ms.Position = 0;
                    bool uploadRes = await _AWSVideoService.UploadFileToS3Async(fileName, ms);
                }

                Video video = new()
                {
                    CourseId = courseId,
                    Path = fileName,

                };

                _dbcontext.Videos.Add(video);
                await _dbcontext.SaveChangesAsync();

                res.Video1 = video;
                res.Message = "Video succesfull posted.";
                res.Status = HttpStatusCode.OK;
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while posting the video: {ex.Message}";
                return res;
            }
        }
    }
}