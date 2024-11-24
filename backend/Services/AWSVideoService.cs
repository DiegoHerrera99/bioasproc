
using System.Net;
using Amazon.S3;
using Amazon.S3.Model;

namespace bioinsumos_asproc_backend.Services
{
    public class AWSVideoService : IAWSService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string S3Bucket;
        private readonly string S3BucketVideoFolder;
        private readonly string S3CDN;

        public AWSVideoService(IConfiguration config, IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
            S3Bucket = config.GetValue<string>("AWS:S3Bucket");
            S3BucketVideoFolder = config.GetValue<string>("AWS:S3BucketVideoFolder");
            S3CDN = config.GetValue<string>("AWS:S3CDN");
        }

        public async Task<bool> DeleteFileFromS3Async(string path)
        {
            try
            {
                string keyName = $"{S3BucketVideoFolder}/{path}";
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

        public async Task<byte[]> DownloadFileFromS3Async(string path)
        {
            try
            {
                string keyName = $"{S3BucketVideoFolder}/{path}";
                var request = new GetObjectRequest
                {
                    BucketName = S3Bucket,
                    Key = keyName
                };

                var response = await _s3Client.GetObjectAsync(request);
                using var ms = new MemoryStream();
                await response.ResponseStream.CopyToAsync(ms);
                return ms.ToArray();
            }
            catch (AmazonS3Exception ex)
            {
                throw ex;
            }
        }

        public string GetVideoStreamingFromS3Async(string path)
        {
            return $"{S3CDN}/video/{path}";
        }

        public async Task<bool> UploadFileToS3Async(string path, Stream fileStream)
        {
            try
            {
                string keyName = $"{S3BucketVideoFolder}/{path}";
                var putRequest = new PutObjectRequest
                {
                    BucketName = S3Bucket,
                    Key = keyName,
                    InputStream = fileStream,
                    ContentType = "video/mp4"
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