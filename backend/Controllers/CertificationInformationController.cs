using System.Net;
using bioinsumos_asproc_backend.Models;
using bioinsumos_asproc_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bioinsumos_asproc_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificationInformationController : ControllerBase
    {
        private readonly ICertificationInformationService _certificationInformationService;

        public CertificationInformationController(ICertificationInformationService certificationInformationService)
        {
            _certificationInformationService = certificationInformationService;
        }


        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _certificationInformationService.GetCertificationInformation();
            if (list == null) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Ok(list);
        }

        [HttpGet("{certificationInformationId}")]
        public async Task<IActionResult> GetById(uint certificationInformationId)
        {
            var certificationInformation = await _certificationInformationService.GetCertificationInformationById(certificationInformationId);
            if (certificationInformation == null) return NotFound();
            return Ok(certificationInformation);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CertifionInformation certificationInformation)
        {
            var res = await _certificationInformationService.CreateCertificationInformation(certificationInformation);
            if (res == null) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Created("", res);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] CertifionInformation _certificationInformation)
        {
            var certificationInformation = await _certificationInformationService.GetCertificationInformationById(_certificationInformation.CertificationInformationId);
            if (certificationInformation == null) return NotFound();

            var res = await _certificationInformationService.UpdateCertificationInformation(_certificationInformation, certificationInformation);
            if (res == null) return StatusCode((int)HttpStatusCode.InternalServerError);

            return Ok(res);
        }

        [HttpDelete("{certificationInformationId}")]
        [Authorize]
        public async Task<IActionResult> Delete(uint certificationInformationId)
        {
            var certificationInformation = await _certificationInformationService.GetCertificationInformationById(certificationInformationId);
            if (certificationInformation == null) return NotFound();

            var res = await _certificationInformationService.DeleteCertificationInformation(certificationInformation);
            if (!res) return StatusCode((int)HttpStatusCode.InternalServerError);
            return NoContent();
        }
    }
}