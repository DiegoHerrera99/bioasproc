using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public interface IFaqService
    {
        Task<List<Faq>> GetFaqs(); //GET
        Task<Faq> GetFaqById(uint faqId); //GET BY ID
        Task<Faq> CreateFaq(Faq faq); //POST
        Task<Faq> UpdateFaq(Faq _faq, Faq faq); //PUT
        Task<bool> DeleteFaq(Faq faq); //DELETE
    }
}