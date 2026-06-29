using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;

namespace Filmoteka.API.Services.Sala
{
    public interface ISalaService
    {
        public Task<ServiceResult<List<Models.Sala>>> GetSaleAsync();
        public Task<ServiceResult<Models.Sala>> GetSalaByIdAsync(int id);
        public Task<ServiceResult<int>> CreateSalaAsync(CreateSalaDTO sala);
        public Task<ServiceResult> DeleteSalaAsync(int id);
    }
}
