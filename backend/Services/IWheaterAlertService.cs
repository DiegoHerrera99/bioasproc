using bioinsumos_asproc_backend.Models;

namespace bioinsumos_asproc_backend.Services
{
    public interface IWheaterAlertService
    {
        Task<List<WheaterAlert>> GetWheaterAlerts(); //GET LIST
        Task<WheaterAlert> GetWheaterAlertById(uint wheaterAlertId); //GET BY ID
        string GetWheaterAlertCDN(string path);
        Task<WheaterAlertDtoCreateResponse> CreateWheaterAlert(WheaterAlert wheaterAlert, IFormFile file); //CREATE 
        Task<WheaterAlertDtoUpdateResponse> UpdateWheaterAlert(WheaterAlert _wheaterAlert, WheaterAlert wheaterAlert, IFormFile file = null); //UPDATE
        Task<WheaterAlertDtoDeleteResponse> DeleteWheaterAlert(WheaterAlert wheaterAlert); //DELETE
    }
}