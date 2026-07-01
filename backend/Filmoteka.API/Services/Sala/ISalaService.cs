using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;

namespace Filmoteka.API.Services.Sala
{
    public interface ISalaService
    {
        public Task<ServiceResult<List<Models.Sala>>> GetAllAsync();
        public Task<ServiceResult<Models.Sala>> GetByIdAsync(int id);
        public Task<ServiceResult<int>> CreateAsync(CreateSalaDTO sala);
        public Task<ServiceResult> DeleteAsync(int id);
    }
}
