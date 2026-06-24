using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;
using praktika1.Models;

namespace MainProjectOOPIII3.Services.Account
{
    public class AuthService : IAuthService
    {
        private MyAppContext _context;
        public AuthService(MyAppContext context)
        {
            _context = context;
        }

        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        private bool VerifyPassword(string hash, string password)
        {
            return _passwordHasher.VerifyHashedPassword(null, hash, password) == PasswordVerificationResult.Success;
        }

        public async Task<ServiceResult<User>> RegisterAsync(RegisterValidationModel model)
        {
            bool usernameExists = await _context.Users.AnyAsync(u => u.Username == model.Username);
            if (usernameExists)
            {
                return ServiceResult<User>.Greska("Korisničko ime je već zauzeto.", "Username");
            }

            bool emailExists = await _context.Users.AnyAsync(u => u.Email == model.Email);
            if (emailExists)
            {
                return ServiceResult<User>.Greska("Email adresa je već zauzeta.", "Email");
            }

            User newUser = new User
            {
                Username = model.Username,
                Email = model.Email,
                Role = Role.User
            };

            newUser.PasswordHash = HashPassword(model.Password);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return ServiceResult<User>.Ok(newUser);
        }

        public async Task<ServiceResult<User>> LoginAsync(LoginValidationModel model)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.UsernameOrEmail || u.Email == model.UsernameOrEmail);

            if (user == null || !VerifyPassword(user.PasswordHash, model.Password))
            {
                return ServiceResult<User>.Greska("Neispravno ime/email ili lozinka.", "UsernameOrEmail");
            }

            return ServiceResult<User>.Ok(user);
        }
    }
}
