using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;

namespace Filmoteka.API.Services.Termin
{
    public interface ITerminService
    {
        public Task<ServiceResult<List<TerminInfoDTO>>> GetTerminiInfoAsync();
        public Task<ServiceResult<TerminInfoDTO>> GetTerminInfoByIdAsync(int id);
        public Task<ServiceResult<int>> CreateTerminAsync(CreateTerminDTO termin);
        public Task<ServiceResult> UpdateTerminAsync(int id, CreateTerminDTO termin);
        public Task<ServiceResult> DeleteTerminAsync(int id);
        public Task<ServiceResult> RezervisiAsync(int terminId, int korisnikId);
    }
}
