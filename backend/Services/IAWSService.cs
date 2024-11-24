namespace bioinsumos_asproc_backend.Services
{
    public interface IAWSService
    {
        Task<bool> UploadFileToS3Async(string path, Stream fileStream);
        Task<byte[]> DownloadFileFromS3Async(string path);
        Task<bool> DeleteFileFromS3Async(string path);
        string GetVideoStreamingFromS3Async(string path);
    }
}