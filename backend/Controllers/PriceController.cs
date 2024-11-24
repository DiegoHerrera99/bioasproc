using System.Net;
using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly IPriceService priceService;
        private readonly List<string> validImageTypes = new() { "image/jpg", "image/jpeg", "image/png" };

        public PriceController(IPriceService priceService)
        {
            this.priceService = priceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrice()
        {
            var list = await priceService.GetPrice();
            if (list == null) return StatusCode((int)HttpStatusCode.InternalServerError);

            var _list = list.Select(n => 
            {
                n.ImgPath = priceService.GetPriceCDN(n.ImgPath);
                return n;
            }).ToArray();
            
            return Ok(_list);
        }

        [HttpGet("{priceId}")]
        public async Task<IActionResult> GetPriceById(uint priceId)
        {
            var price = await priceService.GetPriceById(priceId);
            if (price == null) return NotFound(new { Message = "priceId not found." });

            price.ImgPath = priceService.GetPriceCDN(price.ImgPath);
            return Ok(price);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostPrice(
            IFormFile file, 
            [FromForm] string title,
            [FromForm] string description,
            [FromForm] string price
        )
        {
            try
            {
                if (file == null || file.Length == 0) return BadRequest(new { Message = "file is required" });
                if (!validImageTypes.Contains(file.ContentType)) return BadRequest(new { Message = "file must be .jpg or .png" });
                if (title == null || title.Length == 0) return BadRequest(new { Message = "title is required" });
                if (description == null || description.Length == 0) return BadRequest(new { Message = "description is required" });

                ushort? _ushortPrice = null; // el valor final como ushort?
                if (ushort.TryParse(price, out ushort tempResult))
                {
                    _ushortPrice = tempResult; // asigna el valor convertido si es válido
                }

                Price _price = new()
                {
                    Title = title,
                    Description = description
                };

                if (_ushortPrice != null) _price.Price1 = _ushortPrice;

                var res = await priceService.CreatePrice(_price, file);
                if (res.Status == HttpStatusCode.InternalServerError) return StatusCode((int) res.Status, res);
                
                return CreatedAtAction(nameof(GetPriceById), new { priceId = res.Price.PriceId }, res.Price);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdatePrice(
            IFormFile file, 
            [FromForm] string priceId, 
            [FromForm] string title, 
            [FromForm] string description,
            [FromForm] string newPrice
        )
        {
            try
            {
                if (priceId == null) return BadRequest(new { Message = "priceId is required" });

                IFormFile _file = null;
                if (file != null && file.Length > 0) 
                {
                    _file = file; 
                    if (!validImageTypes.Contains(_file.ContentType)) return BadRequest(new { Message = "file must be .jpg or .png" });
                }

                uint _priceId = uint.Parse(priceId);
                var price = await priceService.GetPriceById(_priceId);
                if (price == null) return NotFound(new { Message = "priceId not found." });

                ushort? _ushortPrice = null; // el valor final como ushort?
                if (ushort.TryParse(newPrice, out ushort tempResult))
                {
                    _ushortPrice = tempResult; // asigna el valor convertido si es válido
                }

                var _price = new Price();
                if (title != null && title.Length > 0) _price.Title = title;
                if (description != null && description.Length > 0) _price.Description = description;
                if (_ushortPrice != null) _price.Price1 = _ushortPrice;

                var res = await priceService.UpdatePrice(_price, price, _file);
                if (res.Status != HttpStatusCode.OK) return StatusCode((int) res.Status, res);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpDelete("{priceId}")]
        [Authorize]
        public async Task<IActionResult> DeletePrice(uint priceId)
        {
            try
            {
                var price = await priceService.GetPriceById(priceId);
                if (price == null) return NotFound(new { Message = "priceId not found." });

                var res = await priceService.DeletePrice(price);
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