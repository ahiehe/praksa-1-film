using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;

namespace Filmoteka.API.Services.Zanr
{
    public interface IZanrService
    {
        Task<ServiceResult<List<praktika1.Models.Zanr>>> GetAllAsync();
        Task<ServiceResult<int>> CreateAsync(CreateZanrDTO dto);
        Task<ServiceResult> UpdateAsync(int id, CreateZanrDTO dto);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
