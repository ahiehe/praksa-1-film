using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;

namespace Filmoteka.API.Services.Reziser
{
    public interface IReziserService
    {
        Task<ServiceResult<List<ReziserDTO>>> GetAllAsync();
        Task<ServiceResult<int>> CreateAsync(CreateReziserDTO dto);
        Task<ServiceResult> UpdateAsync(int id, CreateReziserDTO dto);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
