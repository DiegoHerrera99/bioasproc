using bioinsumos_asproc_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace bioinsumos_asproc_backend.Services
{
    public class CertificationInformationService : ICertificationInformationService
    {
        private readonly BioinsumosContext _dbcontext;

        public CertificationInformationService(BioinsumosContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<CertifionInformation> CreateCertificationInformation(CertifionInformation certifionInformation)
        {
            try
            {
                _dbcontext.CertifionInformations.Add(certifionInformation);
                await _dbcontext.SaveChangesAsync();
                return certifionInformation;
            }
            catch (Exception)
            {
                return await Task.FromException<CertifionInformation>(null);
            }
        }

        public async Task<bool> DeleteCertificationInformation(CertifionInformation certificationInformation)
        {
            try
            {
                certificationInformation.Status = false;
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<CertifionInformation>> GetCertificationInformation()
        {
            try
            {
                return await _dbcontext.CertifionInformations
                    .Where(c => c.Status == true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<List<CertifionInformation>>(null);
            }
        }

        public async Task<CertifionInformation> GetCertificationInformationById(uint certificationInformationId)
        {
            try 
            {
                return await _dbcontext.CertifionInformations
                    .Where(c =>
                        c.CertificationInformationId == certificationInformationId &&
                        c.Status == true
                    )
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<CertifionInformation>(null);
            }
        }

        public async Task<CertifionInformation> UpdateCertificationInformation(CertifionInformation _certificationInformation, CertifionInformation certificationInformation)
        {
            try
            {
                if (_certificationInformation.Title != null && _certificationInformation.Title != certificationInformation.Title) certificationInformation.Title = _certificationInformation.Title;
                if (_certificationInformation.Body != null && _certificationInformation.Body != certificationInformation.Body) certificationInformation.Body = _certificationInformation.Body;
                await _dbcontext.SaveChangesAsync();
                return certificationInformation;
            }
            catch (Exception)
            {
                return await Task.FromException<CertifionInformation>(null);
            }
        }
    }
}