using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public interface IVideoService
    {
        Task<VideoDtoPostResponse> UploadVideo(IFormFile file, uint courseId);
        VideoDtoStreamResponse StreamVideo(string path);
        Task<VideoDtoGetResponse> DownloadVideo(string path);
        Task<VideoDtoDeleteResponse> DeleteVideo(Video video);
        Task<VideoDtoPutResponse> UpdateVideo(IFormFile file, Video video);
    }
}