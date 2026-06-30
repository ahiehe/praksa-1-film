using Filmoteka.API.DTOs;

namespace MainProjectOOPIII3.Services.Account
{
    public interface IAuthService
    {
        public Task<ServiceResult<AuthResponseDTO>> RegisterAsync(RegisterDTO model);
        public Task<ServiceResult<AuthResponseDTO>> LoginAsync(LoginDTO model);
    }
}
