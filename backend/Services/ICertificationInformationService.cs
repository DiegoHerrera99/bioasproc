using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public interface ICertificationInformationService
    {
        Task<List<CertifionInformation>> GetCertificationInformation(); //GET
        Task<CertifionInformation> GetCertificationInformationById(uint certificationInformationId); //GET BY ID
        Task<CertifionInformation> CreateCertificationInformation(CertifionInformation certifionInformation); //POST
        Task<CertifionInformation> UpdateCertificationInformation(CertifionInformation _certificationInformation, CertifionInformation certificationInformation); //PUT
        Task<bool> DeleteCertificationInformation(CertifionInformation certificationInformation); //DELETE
    }
}