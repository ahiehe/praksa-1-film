using Filmoteka.API.DTOs;
using praktika1.Models;

namespace MainProjectOOPIII3.Services.Account
{
    public interface IAuthService
    {
        public Task<ServiceResult<AuthResponseDTO>> RegisterAsync(RegisterValidationModel model);
        public Task<ServiceResult<AuthResponseDTO>> LoginAsync(LoginValidationModel model);
    }
}
