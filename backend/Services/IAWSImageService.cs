namespace bioinsumos_asproc_backend.Services
{
    public interface IAWSImageService
    {
        Task<bool> UploadImageToS3Async(string path, string S3Folder, string contentType, Stream fileStream);
        string DownloadImageFromS3Async(string path, string S3Folder);
        Task<bool> DeleteImageFromS3Async(string path, string S3Folder);
    }
}