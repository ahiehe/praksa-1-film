using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;

namespace Filmoteka.API.Services.Termin
{
    public interface ITerminService
    {
        public Task<ServiceResult<List<TerminInfoDTO>>> GetActiveTerminiInfoAsync();
        public Task<ServiceResult<TerminDetailsDTO>> GetTerminDetailsByIdAsync(int id, int korisnikId);
        public Task<ServiceResult<int>> CreateTerminAsync(CreateTerminDTO termin);
        public Task<ServiceResult> UpdateTerminAsync(int id, CreateTerminDTO termin);
        public Task<ServiceResult> DeleteTerminAsync(int id);
        public Task<ServiceResult> RezervisiAsync(int terminId, int korisnikId);
    }
}
