using System.Net;
using bioinsumos_asproc_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace bioinsumos_asproc_backend.Services
{
    public class ProductService : IProductService
    {
        private readonly string S3BucketProductFolder;
        private readonly BioinsumosContext _dbcontext;
        private readonly IAWSImageService _AWSImageService;

        public ProductService(IConfiguration config, BioinsumosContext dbcontext, IAWSImageService aWSImageService)
        {
            S3BucketProductFolder = config.GetValue<string>("AWS:S3BucketProductFolder");
            _dbcontext = dbcontext;
            _AWSImageService = aWSImageService;
        }

        public async Task<ProductDtoCreateResponse> CreateProduct(Product product, IFormFile file)
        {
            ProductDtoCreateResponse res = new ();
            try
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    bool uploadToS3res = await _AWSImageService.UploadImageToS3Async(fileName, S3BucketProductFolder, file.ContentType, stream);
                }

                var _product = new Product
                {
                    Title = product.Title,
                    Description = product.Description,
                    ImgPath = fileName
                };

                _dbcontext.Products.Add(_product);
                await _dbcontext.SaveChangesAsync();

                res.Status = HttpStatusCode.OK;
                res.Message = "OK";
                res.Product = _product;
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = ex.InnerException.Message;
                return res;
            }
        }

        public async Task<ProductDtoDeleteResponse> DeleteProduct(Product product)
        {
            ProductDtoDeleteResponse res = new();
            try
            {
                var deleteRes = await _AWSImageService.DeleteImageFromS3Async(product.ImgPath, S3BucketProductFolder);
                if (!deleteRes)
                {
                    res.Status = HttpStatusCode.InternalServerError;
                    res.Message = "An error occurred while deleting the product.";
                    return res;
                }

                _dbcontext.Products.Remove(product);
                await _dbcontext.SaveChangesAsync();
                res.Status = HttpStatusCode.OK;
                res.Message = "Ok";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while deleting the product: {ex.Message}";
                return res;
            }
        }

        public async Task<List<Product>> GetProduct()
        {
            try
            {
                return await _dbcontext.Products
                    .Where(p => p.Status == true)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return await Task.FromException<List<Product>>(null);
            }
        }

        public string GetProductCDN(string path)
        {
            return _AWSImageService.DownloadImageFromS3Async(path, S3BucketProductFolder);
        }

        public async Task<Product> GetProductById(uint productId)
        {
            try 
            {
                return await _dbcontext.Products
                    .Where(p =>
                        p.ProductId == productId &&
                        p.Status == true
                    )
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<Product>(null);
            }
        }

        public async Task<ProductDtoUpdateResponse> UpdateProduct(Product _product, Product product, IFormFile file = null)
        {
            ProductDtoUpdateResponse res = new();
            try
            {
                if (file != null) {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        stream.Position = 0;
                        bool uploadToS3res = await _AWSImageService.UploadImageToS3Async(product.ImgPath, S3BucketProductFolder, file.ContentType, stream);
                    }
                }

                if (_product.Title != null && _product.Title != product.Title) product.Title = _product.Title;
                if (_product.Description != null && _product.Description != product.Description) product.Description = _product.Description;
                await _dbcontext.SaveChangesAsync();

                res.Status = HttpStatusCode.OK;
                res.Message = "Product updated successfully.";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while updating product: {ex.Message}";
                return res;
            }
        }
    }
}