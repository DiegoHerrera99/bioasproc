using System.Net;
using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaqController : ControllerBase
    {
        private readonly IFaqService _faqService;

        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }


        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _faqService.GetFaqs();
            if (list == null) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Ok(list);
        }

        [HttpGet("{faqId}")]
        public async Task<IActionResult> GetById(uint faqId)
        {
            var faq = await _faqService.GetFaqById(faqId);
            if (faq == null) return NotFound();
            return Ok(faq);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(Faq faq)
        {
            var res = await _faqService.CreateFaq(faq);
            if (res == null) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Created("", res);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] Faq _faq)
        {
            var faq = await _faqService.GetFaqById(_faq.FaqId);
            if (faq == null) return NotFound();

            var res = await _faqService.UpdateFaq(_faq, faq);
            if (res == null) return StatusCode((int)HttpStatusCode.InternalServerError);

            return Ok(res);
        }

        [HttpDelete("{faqId}")]
        [Authorize]
        public async Task<IActionResult> Delete(uint faqId)
        {
            var faq = await _faqService.GetFaqById(faqId);
            if (faq == null) return NotFound();

            var res = await _faqService.DeleteFaq(faq);
            if (!res) return StatusCode((int)HttpStatusCode.InternalServerError);
            return NoContent();
        }
    }
}