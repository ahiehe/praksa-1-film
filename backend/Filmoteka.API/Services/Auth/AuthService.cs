using Filmoteka.API.DTOs;
using Filmoteka.API.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;
using praktika1.Models;

namespace MainProjectOOPIII3.Services.Account
{
    public class AuthService : IAuthService
    {
        private MyAppContext _context;
        private JwtService _jwtService;

        public AuthService(MyAppContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
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

        public async Task<ServiceResult<AuthResponseDTO>> RegisterAsync(RegisterValidationModel model)
        {
            bool usernameExists = await _context.Users.AnyAsync(u => u.Username == model.Username);
            if (usernameExists)
            {
                return ServiceResult<AuthResponseDTO>.Greska("Korisničko ime je već zauzeto.");
            }

            bool emailExists = await _context.Users.AnyAsync(u => u.Email == model.Email);
            if (emailExists)
            {
                return ServiceResult<AuthResponseDTO>.Greska("Email adresa je već zauzeta.");
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

            return ServiceResult<AuthResponseDTO>.Ok(new AuthResponseDTO
            {
                Token = _jwtService.GenerateToken(newUser),
                Username = newUser.Username
            });
        }

        public async Task<ServiceResult<AuthResponseDTO>> LoginAsync(LoginValidationModel model)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.UsernameOrEmail || u.Email == model.UsernameOrEmail);

            if (user == null || !VerifyPassword(user.PasswordHash, model.Password))
            {
                return ServiceResult<AuthResponseDTO>.Greska("Neispravno ime/email ili lozinka.");
            }

            return ServiceResult<AuthResponseDTO>.Ok(new AuthResponseDTO
            {
                Token = _jwtService.GenerateToken(user),
                Username = user.Username
            });
        }
    }
}
