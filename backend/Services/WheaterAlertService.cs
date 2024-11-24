using System.Net;
using bioinsumos_asproc_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace bioinsumos_asproc_backend.Services
{
    public class WheaterAlertService : IWheaterAlertService
    {
        private readonly string S3BucketWheaterAlertFolder;
        private readonly BioinsumosContext _dbcontext;
        private readonly IAWSImageService _AWSImageService;

        public WheaterAlertService(IConfiguration config, BioinsumosContext dbcontext, IAWSImageService aWSImageService)
        {
            S3BucketWheaterAlertFolder = config.GetValue<string>("AWS:S3BucketWheaterAlertFolder");
            _dbcontext = dbcontext;
            _AWSImageService = aWSImageService;
        }

        public async Task<WheaterAlertDtoCreateResponse> CreateWheaterAlert(WheaterAlert wheaterAlert, IFormFile file)
        {
            WheaterAlertDtoCreateResponse res = new ();
            try
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    bool uploadToS3res = await _AWSImageService.UploadImageToS3Async(fileName, S3BucketWheaterAlertFolder, file.ContentType, stream);
                }

                var _wheaterAlert = new WheaterAlert
                {
                    Title = wheaterAlert.Title,
                    Body = wheaterAlert.Body,
                    ImgPath = fileName
                };

                if (wheaterAlert.Url != null) _wheaterAlert.Url = wheaterAlert.Url;

                _dbcontext.WheaterAlerts.Add(_wheaterAlert);
                await _dbcontext.SaveChangesAsync();

                res.Status = HttpStatusCode.OK;
                res.Message = "OK";
                res.WheaterAlert = _wheaterAlert;
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = ex.InnerException.Message;
                return res;
            }
        }

        public async Task<WheaterAlertDtoDeleteResponse> DeleteWheaterAlert(WheaterAlert wheaterAlert)
        {
            WheaterAlertDtoDeleteResponse res = new();
            try
            {
                var deleteRes = await _AWSImageService.DeleteImageFromS3Async(wheaterAlert.ImgPath, S3BucketWheaterAlertFolder);
                if (!deleteRes)
                {
                    res.Status = HttpStatusCode.InternalServerError;
                    res.Message = "An error occurred while deleting the wheaterAlert.";
                    return res;
                }

                _dbcontext.WheaterAlerts.Remove(wheaterAlert);
                await _dbcontext.SaveChangesAsync();
                res.Status = HttpStatusCode.OK;
                res.Message = "Ok";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while deleting the wheaterAlert: {ex.Message}";
                return res;
            }
        }

        public async Task<List<WheaterAlert>> GetWheaterAlerts()
        {
            try
            {
                return await _dbcontext.WheaterAlerts
                    .Where(n => n.Status == true)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return await Task.FromException<List<WheaterAlert>>(null);
            }
        }

        public string GetWheaterAlertCDN(string path)
        {
            return _AWSImageService.DownloadImageFromS3Async(path, S3BucketWheaterAlertFolder);
        }

        public async Task<WheaterAlert> GetWheaterAlertById(uint wheaterAlertId)
        {
            try 
            {
                return await _dbcontext.WheaterAlerts
                    .Where(n =>
                        n.WeatherAlertId == wheaterAlertId &&
                        n.Status == true
                    )
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<WheaterAlert>(null);
            }
        }

        public async Task<WheaterAlertDtoUpdateResponse> UpdateWheaterAlert(WheaterAlert _wheaterAlert, WheaterAlert wheaterAlert, IFormFile file = null)
        {
            WheaterAlertDtoUpdateResponse res = new();
            try
            {
                if (file != null) {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        stream.Position = 0;
                        bool uploadToS3res = await _AWSImageService.UploadImageToS3Async(wheaterAlert.ImgPath, S3BucketWheaterAlertFolder, file.ContentType, stream);
                    }
                }

                if (_wheaterAlert.Title != null && _wheaterAlert.Title != wheaterAlert.Title) wheaterAlert.Title = _wheaterAlert.Title;
                if (_wheaterAlert.Body != null && _wheaterAlert.Body != wheaterAlert.Body) wheaterAlert.Body = _wheaterAlert.Body;
                if (_wheaterAlert.Url != null && _wheaterAlert.Url != wheaterAlert.Url) wheaterAlert.Url = _wheaterAlert.Url;
                await _dbcontext.SaveChangesAsync();

                res.Status = HttpStatusCode.OK;
                res.Message = "WheaterAlert updated successfully.";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while updating wheaterAlert: {ex.Message}";
                return res;
            }
        }
    }
}