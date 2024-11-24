using System.Net;
using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public class PdfService : IPdfService
    {
        private readonly BioinsumosContext _dbcontext;
        private readonly IAWSService _AWSPdfService;
        public PdfService(BioinsumosContext dbcontext, IServiceProvider serviceProvider)
        {
            _dbcontext = dbcontext;
           _AWSPdfService = serviceProvider
                .GetServices<IAWSService>()
                .OfType<AWSPdfService>()
                .FirstOrDefault();
        }

        public async Task<PdfDtoGetResponse> FetchFile(string path)
        {
            PdfDtoGetResponse res = new();
            try
            {
                var fileBytes = await _AWSPdfService.DownloadFileFromS3Async(path);
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

        public async Task<PdfDtoPostResponse> UploadFile(IFormFile file, uint courseId)
        {
            PdfDtoPostResponse res = new();
            try
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    bool uploadToS3res = await _AWSPdfService.UploadFileToS3Async(fileName, stream);
                }

                var pdf = new Pdf
                {
                    CourseId = courseId,
                    Path = fileName
                };

                _dbcontext.Pdfs.Add(pdf);
                await _dbcontext.SaveChangesAsync();

                res.Status = HttpStatusCode.OK;
                res.Message = "OK";
                res.Pdf1 = pdf;
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = ex.Message;
                return res;
            }
        }

        public async Task<PdfDtoDeleteResponse> DeleteFile(Pdf pdf)
        {
            PdfDtoDeleteResponse res = new();
            try
            {
                var deleteRes = await _AWSPdfService.DeleteFileFromS3Async(pdf.Path);
                if (!deleteRes)
                {
                    res.Status = HttpStatusCode.InternalServerError;
                    res.Message = "An error occurred while deleting the file.";
                    return res;
                }

                _dbcontext.Pdfs.Remove(pdf);
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

        public async Task<PdfDtoPutResponse> UpdateFile(IFormFile file, Pdf pdf)
        {
            PdfDtoPutResponse res = new();
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    bool uploadToS3res = await _AWSPdfService.UploadFileToS3Async(pdf.Path, stream);
                }

                //Forzar actualización de modifiedAt
                pdf.Status = false;
                await _dbcontext.SaveChangesAsync();
                pdf.Status = true;
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
    }
}