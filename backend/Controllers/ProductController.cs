using System.Net;
using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly List<string> validImageTypes = new() { "image/jpg", "image/jpeg", "image/png" };

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var list = await productService.GetProduct();
            if (list == null) return StatusCode((int)HttpStatusCode.InternalServerError);

            var _list = list.Select(n => 
            {
                n.ImgPath = productService.GetProductCDN(n.ImgPath);
                return n;
            }).ToArray();
            
            return Ok(_list);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(uint productId)
        {
            var product = await productService.GetProductById(productId);
            if (product == null) return NotFound(new { Message = "productId not found." });

            product.ImgPath = productService.GetProductCDN(product.ImgPath);
            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostProduct(
            IFormFile file, 
            [FromForm] string title,
            [FromForm] string description
        )
        {
            try
            {
                if (file == null || file.Length == 0) return BadRequest(new { Message = "file is required" });
                if (!validImageTypes.Contains(file.ContentType)) return BadRequest(new { Message = "file must be .jpg or .png" });
                if (title == null || title.Length == 0) return BadRequest(new { Message = "title is required" });
                if (description == null || description.Length == 0) return BadRequest(new { Message = "description is required" });

                Product _product = new()
                {
                    Title = title,
                    Description = description
                };

                var res = await productService.CreateProduct(_product, file);
                if (res.Status == HttpStatusCode.InternalServerError) return StatusCode((int) res.Status, res);
                
                return CreatedAtAction(nameof(GetProductById), new { productId = res.Product.ProductId }, res.Product);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(
            IFormFile file, 
            [FromForm] string productId, 
            [FromForm] string title, 
            [FromForm] string description
        )
        {
            try
            {
                if (productId == null) return BadRequest(new { Message = "productId is required" });

                IFormFile _file = null;
                if (file != null && file.Length > 0) 
                {
                    _file = file; 
                    if (!validImageTypes.Contains(_file.ContentType)) return BadRequest(new { Message = "file must be .jpg or .png" });
                }

                uint _productId = uint.Parse(productId);
                var product = await productService.GetProductById(_productId);
                if (product == null) return NotFound(new { Message = "productId not found." });

                var _product = new Product();
                if (title != null && title.Length > 0) _product.Title = title;
                if (description != null && description.Length > 0) _product.Description = description;

                var res = await productService.UpdateProduct(_product, product, _file);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpDelete("{productId}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(uint productId)
        {
            try
            {
                var product = await productService.GetProductById(productId);
                if (product == null) return NotFound(new { Message = "productId not found." });

                var res = await productService.DeleteProduct(product);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
    }
}