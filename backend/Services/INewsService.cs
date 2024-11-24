using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public interface INewsService
    {
        Task<List<News>> GetNews(); //GET LIST
        Task<News> GetNewsById(uint newsId); //GET BY ID
        string GetNewsCDN(string path);
        Task<NewsDtoCreateResponse> CreateNews(News news, IFormFile file); //CREATE 
        Task<NewsDtoUpdateResponse> UpdateNews(News _news, News news, IFormFile file = null); //UPDATE
        Task<NewsDtoDeleteResponse> DeleteNews(News news); //DELETE
    }
}