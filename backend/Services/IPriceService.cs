using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public interface IPriceService
    {
        Task<List<Price>> GetPrice(); //GET LIST
        Task<Price> GetPriceById(uint priceId); //GET BY ID
        string GetPriceCDN(string path);
        Task<PriceDtoCreateResponse> CreatePrice(Price price, IFormFile file); //CREATE 
        Task<PriceDtoUpdateResponse> UpdatePrice(Price _price, Price price, IFormFile file = null); //UPDATE
        Task<PriceDtoDeleteResponse> DeletePrice(Price price); //DELETE
    }
}