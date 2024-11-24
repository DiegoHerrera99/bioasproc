using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public interface IHandbookService
    {
        Task<Handbook> GetHandbookById(uint handbookId);
        Task<List<Handbook>> GetHandbooks();
        Task<HandbookDtoUpdateResponse> UpdateHandbook(Handbook _handbook, Handbook handbook, IFormFile file = null);
        Task<HandbookDtoDeleteResponse> DeleteHandbook(Handbook handbook);
        Task<HandbookDtoUploadResponse> UploadHandbook(IFormFile file, string name, string description);
        Task<HandbookDtoDownloadResponse> DownloadHandbook(string path);
    }
}