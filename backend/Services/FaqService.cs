using bioinsumos_asproc_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace bioinsumos_asproc_backend.Services
{
    public class FaqService : IFaqService
    {
        private readonly BioinsumosContext _dbcontext;

        public FaqService(BioinsumosContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Faq> CreateFaq(Faq faq)
        {
            try
            {
                _dbcontext.Faqs.Add(faq);
                await _dbcontext.SaveChangesAsync();
                return faq;
            }
            catch (Exception)
            {
                return await Task.FromException<Faq>(null);
            }
        }

        public async Task<bool> DeleteFaq(Faq faq)
        {
            try
            {
                faq.Status = false;
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Faq> GetFaqById(uint faqId)
        {
            try 
            {
                return await _dbcontext.Faqs
                    .Where(c =>
                        c.FaqId == faqId &&
                        c.Status == true
                    )
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<Faq>(null);
            }
        }

        public async Task<List<Faq>> GetFaqs()
        {
            try
            {
                return await _dbcontext.Faqs
                    .Where(f => f.Status == true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return await Task.FromException<List<Faq>>(null);
            }
        }

        public async Task<Faq> UpdateFaq(Faq _faq, Faq faq)
        {
            try
            {
                if (_faq.Question != null && _faq.Question != faq.Question) faq.Question = _faq.Question;
                if (_faq.Answer != null && _faq.Answer != faq.Answer) faq.Answer = _faq.Answer;
                await _dbcontext.SaveChangesAsync();
                return faq;
            }
            catch (Exception)
            {
                return await Task.FromException<Faq>(null);
            }
        }
    }
}