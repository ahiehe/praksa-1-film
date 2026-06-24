using praktika1.Models;

namespace MainProjectOOPIII3.Services.Account
{
    public interface IAuthService
    {
        public Task<ServiceResult<User>> RegisterAsync(RegisterValidationModel model);
        public Task<ServiceResult<User>> LoginAsync(LoginValidationModel model);
    }
}
