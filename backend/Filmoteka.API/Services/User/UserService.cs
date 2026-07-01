using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;

namespace Filmoteka.API.Services.User
{
    public class UserService : IUserService
    {
        private readonly MyAppContext _context;

        public UserService(MyAppContext context)
        {
            _context = context;
        }

        private readonly PasswordHasher<praktika1.Models.User> _passwordHasher = new PasswordHasher<praktika1.Models.User>();
        private string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public async Task<ServiceResult<int>> CreateAsync(CreateUserDTO dto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username || u.Email == dto.Email);
            if (existingUser != null)
            {
                return ServiceResult<int>.Greska("Korisnik sa istim korisničkim imenom ili email-om već postoji.");
            }

            var user = new praktika1.Models.User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return ServiceResult<int>.Ok(user.Id, "Korisnik je uspešno kreiran.");
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return ServiceResult.Greska("Korisnik nije pronađen.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return ServiceResult.Ok("Korisnik je uspešno obrisan.");
        }

        public async Task<ServiceResult<List<UserInfoDTO>>> GetAllAsync()
        {
            var users = await _context.Users
                .Select(u => new UserInfoDTO
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role
                })
                .ToListAsync();

            return ServiceResult<List<UserInfoDTO>>.Ok(users);
        }

        public async Task<ServiceResult<UserInfoDTO>> GetByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return ServiceResult<UserInfoDTO>.Greska("Korisnik nije pronađen.");
            }

            var userInfo = new UserInfoDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
            return ServiceResult<UserInfoDTO>.Ok(userInfo);
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateUserDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return ServiceResult.Greska("Korisnik nije pronađen.");
            }

            if (dto.Username != null) user.Username = dto.Username;
            if (dto.Email != null) user.Email = dto.Email;
            if (dto.Password != null) user.PasswordHash = HashPassword(dto.Password);
            if (dto.Role != null) user.Role = dto.Role.Value;

            await _context.SaveChangesAsync();
            return ServiceResult.Ok("Korisnik je uspešno ažuriran.");
        }
    }
}
