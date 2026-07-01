using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;

namespace Filmoteka.API.Services.Termin
{
    public interface ITerminService
    {
        public Task<ServiceResult<List<TerminInfoDTO>>> GetActiveTerminiInfoAsync();
        public Task<ServiceResult<TerminDetailsDTO>> GetDetailsByIdAsync(int id, int korisnikId);
        public Task<ServiceResult<int>> CreateAsync(CreateTerminDTO termin);
        public Task<ServiceResult> UpdateAsync(int id, CreateTerminDTO termin);
        public Task<ServiceResult> DeleteAsync(int id);
        public Task<ServiceResult> RezervisiAsync(int terminId, int korisnikId);
    }
}
