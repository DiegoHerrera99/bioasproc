using System.Net;
using bioinsumos_asproc_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace bioinsumos_asproc_backend.Services
{
    public class PriceService : IPriceService
    {
        private readonly string S3BucketPriceFolder;
        private readonly BioinsumosContext _dbcontext;
        private readonly IAWSImageService _AWSImageService;

        public PriceService(IConfiguration config, BioinsumosContext dbcontext, IAWSImageService aWSImageService)
        {
            S3BucketPriceFolder = config.GetValue<string>("AWS:S3BucketPriceFolder");
            _dbcontext = dbcontext;
            _AWSImageService = aWSImageService;
        }

        public async Task<PriceDtoCreateResponse> CreatePrice(Price price, IFormFile file)
        {
            PriceDtoCreateResponse res = new ();
            try
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    bool uploadToS3res = await _AWSImageService.UploadImageToS3Async(fileName, S3BucketPriceFolder, file.ContentType, stream);
                }

                var _price = new Price
                {
                    Title = price.Title,
                    Description = price.Description,
                    ImgPath = fileName
                };

                if (price.Price1 != null) _price.Price1 = price.Price1;

                _dbcontext.Prices.Add(_price);
                await _dbcontext.SaveChangesAsync();

                res.Status = HttpStatusCode.OK;
                res.Message = "OK";
                res.Price = _price;
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = ex.InnerException.Message;
                return res;
            }
        }

        public async Task<PriceDtoDeleteResponse> DeletePrice(Price price)
        {
            PriceDtoDeleteResponse res = new();
            try
            {
                var deleteRes = await _AWSImageService.DeleteImageFromS3Async(price.ImgPath, S3BucketPriceFolder);
                if (!deleteRes)
                {
                    res.Status = HttpStatusCode.InternalServerError;
                    res.Message = "An error occurred while deleting the price.";
                    return res;
                }

                _dbcontext.Prices.Remove(price);
                await _dbcontext.SaveChangesAsync();
                res.Status = HttpStatusCode.OK;
                res.Message = "Ok";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while deleting the price: {ex.Message}";
                return res;
            }
        }

        public async Task<List<Price>> GetPrice()
        {
            try
            {
                return await _dbcontext.Prices
                    .Where(p => p.Status == true)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return await Task.FromException<List<Price>>(null);
            }
        }

        public string GetPriceCDN(string path)
        {
            return _AWSImageService.DownloadImageFromS3Async(path, S3BucketPriceFolder);
        }

        public async Task<Price> GetPriceById(uint priceId)
        {
            try 
            {
                return await _dbcontext.Prices
                    .Where(p =>
                        p.PriceId == priceId &&
                        p.Status == true
                    )
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<Price>(null);
            }
        }

        public async Task<PriceDtoUpdateResponse> UpdatePrice(Price _price, Price price, IFormFile file = null)
        {
            PriceDtoUpdateResponse res = new();
            try
            {
                if (file != null) {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        stream.Position = 0;
                        bool uploadToS3res = await _AWSImageService.UploadImageToS3Async(price.ImgPath, S3BucketPriceFolder, file.ContentType, stream);
                    }
                }

                if (_price.Title != null && _price.Title != price.Title) price.Title = _price.Title;
                if (_price.Description != null && _price.Description != price.Description) price.Description = _price.Description;
                if (_price.Price1 != null && _price.Price1 != price.Price1) price.Price1 = _price.Price1;
                await _dbcontext.SaveChangesAsync();

                res.Status = HttpStatusCode.OK;
                res.Message = "Price updated successfully.";
                return res;
            }
            catch (Exception ex)
            {
                res.Status = HttpStatusCode.InternalServerError;
                res.Message = $"An error occurred while updating price: {ex.Message}";
                return res;
            }
        }
    }
}