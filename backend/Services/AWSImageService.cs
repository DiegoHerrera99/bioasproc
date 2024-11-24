using System.Net;
using Amazon.S3;
using Amazon.S3.Model;

namespace bioinsumos_asproc_backend.Services
{
    public class AWSImageService : IAWSImageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string S3Bucket;
        private readonly string S3CDN;

        public AWSImageService(IConfiguration config, IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
            S3Bucket = config.GetValue<string>("AWS:S3Bucket");
            S3CDN = config.GetValue<string>("AWS:S3CDN");
        }

        public async Task<bool> DeleteImageFromS3Async(string path, string S3Folder)
        {
            try
            {
                string keyName = $"{S3Folder}/{path}";
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = S3Bucket,
                    Key = keyName
                };

                var response = await _s3Client.DeleteObjectAsync(deleteObjectRequest);
                return true;
            }
            catch (AmazonS3Exception ex)
            {
                throw ex;
            }
        }

        public string DownloadImageFromS3Async(string path, string S3Folder)
        {
            return $"{S3CDN}/{S3Folder}/{path}";
        }

        public async Task<bool> UploadImageToS3Async(string path, string S3Folder, string contentType, Stream fileStream)
        {
            try
            {
                string keyName = $"{S3Folder}/{path}";
                var putRequest = new PutObjectRequest
                {
                    BucketName = S3Bucket,
                    Key = keyName,
                    InputStream = fileStream,
                    ContentType = contentType // El tipo de contenido ahora es dinámico, dependiendo del archivo (imagen)
                };

                var response = await _s3Client.PutObjectAsync(putRequest);
                if (response.HttpStatusCode == HttpStatusCode.OK) return true;
                return false;
            }
            catch (AmazonS3Exception ex)
            {
                throw ex;
            }
        }
    }
}
