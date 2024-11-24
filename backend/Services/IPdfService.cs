using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public interface IPdfService
    {
        Task<PdfDtoPostResponse> UploadFile(IFormFile file, uint courseId);
        Task<PdfDtoGetResponse> FetchFile(string path);
        Task<PdfDtoDeleteResponse> DeleteFile(Pdf pdf);
        Task<PdfDtoPutResponse> UpdateFile(IFormFile file, Pdf pdf);
    }
}