using System.Net;
using bioinsumos_asproc_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace bioinsumos_asproc_backend.Services
{
    public class HandbookService : IHandbookService
    {
        private readonly BioinsumosContext _dbcontext;
        private readonly IAWSService _AWSHandbookService;

        public HandbookService(BioinsumosContext dbcontext, IServiceProvider serviceProvider)
        {
            _dbcontext = dbcontext;
           _AWSHandbookService = serviceProvider
                .GetServices<IAWSService>()
                .OfType<AWSHandbookService>()
                .FirstOrDefault();
        }

        public async Task<HandbookDtoDeleteResponse> DeleteHandbook(Handbook handbook)
        {
            HandbookDtoDeleteResponse res = new();
            try
            {
                var deleteRes = await _AWSHandbookService.DeleteFileFromS3Async(handbook.Path);
                if (!deleteRes)
                {
                    res.Status = HttpStatusCode.InternalServerError;
                    res.Message = "An error occurred while deleting the file.";
                    return res;
                }

                _dbcontext.Handbooks.Remove(handbook);
                await _dbcontext.SaveChangesAsync();
                res.Status = HttpStatusCode.OK;
                res.Message = "Ok";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while deleting the file: {ex.Message}";
                return res;
            }
        }

        public async Task<HandbookDtoDownloadResponse> DownloadHandbook(string path)
        {
            HandbookDtoDownloadResponse res = new();
            try
            {
                var fileBytes = await _AWSHandbookService.DownloadFileFromS3Async(path);
                res.FileContent = fileBytes;
                res.FileName = path;
                res.Message = "Ok";
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

        public async Task<Handbook> GetHandbookById(uint handbookId)
        {
            try 
            {
                return await _dbcontext.Handbooks
                    .Where(h =>
                        h.HandbookId == handbookId &&
                        h.Status == true
                    )
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<Handbook>(null);
            }
        }

        public async Task<List<Handbook>> GetHandbooks()
        {
            try
            {
                return await _dbcontext.Handbooks
                    .Where(h => h.Status == true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<List<Handbook>>(null);
            }
        }

        public async Task<HandbookDtoUpdateResponse> UpdateHandbook(Handbook _handbook, Handbook handbook, IFormFile file = null)
        {
            HandbookDtoUpdateResponse res = new();
            try
            {
                if (file != null) {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        stream.Position = 0;
                        bool uploadToS3res = await _AWSHandbookService.UploadFileToS3Async(handbook.Path, stream);
                    }
                }

                if (_handbook.Description != null && _handbook.Description != handbook.Description) handbook.Description = _handbook.Description;
                if (_handbook.Name != null && _handbook.Name != handbook.Name) handbook.Name = _handbook.Name;
                await _dbcontext.SaveChangesAsync();

                res.Status = HttpStatusCode.OK;
                res.Message = "PDF updated successfully.";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while updating the file: {ex.Message}";
                return res;
            }
        }

        public async Task<HandbookDtoUploadResponse> UploadHandbook(IFormFile file, string name, string description)
        {
            HandbookDtoUploadResponse res = new();
            try
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    bool uploadToS3res = await _AWSHandbookService.UploadFileToS3Async(fileName, stream);
                }

                var handbook = new Handbook
                {
                    Name = name,
                    Description = description,
                    Path = fileName
                };

                _dbcontext.Handbooks.Add(handbook);
                await _dbcontext.SaveChangesAsync();

                res.Status = HttpStatusCode.OK;
                res.Message = "OK";
                res.Handbook = handbook;
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = ex.Message;
                return res;
            }
        }
    }
}