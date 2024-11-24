using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProduct(); //GET LIST
        Task<Product> GetProductById(uint priceId); //GET BY ID
        string GetProductCDN(string path);
        Task<ProductDtoCreateResponse> CreateProduct(Product price, IFormFile file); //CREATE 
        Task<ProductDtoUpdateResponse> UpdateProduct(Product _price, Product price, IFormFile file = null); //UPDATE
        Task<ProductDtoDeleteResponse> DeleteProduct(Product price); //DELETE
    }
}