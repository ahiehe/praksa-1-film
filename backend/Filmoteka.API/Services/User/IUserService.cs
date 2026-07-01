using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;

namespace Filmoteka.API.Services.User
{
    public interface IUserService
    {
        public Task<ServiceResult<List<UserInfoDTO>>> GetAllAsync();
        public Task<ServiceResult<UserInfoDTO>> GetByIdAsync(int id);
        public Task<ServiceResult<int>> CreateAsync(CreateUserDTO dto);
        public Task<ServiceResult> UpdateAsync(int id, UpdateUserDTO dto);
        public Task<ServiceResult> DeleteAsync(int id);
    }
}
