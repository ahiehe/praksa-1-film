using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;

namespace Filmoteka.API.Services.Reziser
{
    public class ReziserService : IReziserService
    {
        private readonly MyAppContext _context;
        public ReziserService(MyAppContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<List<ReziserOptionDTO>>> GetOptionsAsync()
        {
            var reziseri = await _context.Reziseri
                 .Select(r => new ReziserOptionDTO { Id = r.Id, Ime = r.Ime, Prezime = r.Prezime })
                 .ToListAsync();
            return ServiceResult<List<ReziserOptionDTO>>.Ok(reziseri);
        }

        public async Task<ServiceResult<List<praktika1.Models.Reziser>>> GetAllAsync()
        {
            var reziseri = await _context.Reziseri.ToListAsync();
            return ServiceResult<List<praktika1.Models.Reziser>>.Ok(reziseri);
        }

        public async Task<ServiceResult<int>> CreateAsync(CreateReziserDTO dto)
        {
            var reziser = new praktika1.Models.Reziser
            {
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                DatumRodjenja = dto.DatumRodjenja
            };
            _context.Reziseri.Add(reziser);
            await _context.SaveChangesAsync();
            return ServiceResult<int>.Ok(reziser.Id);
        }

        public async Task<ServiceResult> UpdateAsync(int id, CreateReziserDTO dto)
        {
            var reziser = await _context.Reziseri.FindAsync(id);
            if (reziser == null) return ServiceResult.Greska("Režiser nije pronađen.");

            reziser.Ime = dto.Ime;
            reziser.Prezime = dto.Prezime;
            reziser.DatumRodjenja = dto.DatumRodjenja;
            await _context.SaveChangesAsync();
            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            bool imaFilmova = await _context.Reziseri
                    .Where(r => r.Id == id)
                    .AnyAsync(r => r.Filmovi.Any());
            if (imaFilmova)
                return ServiceResult.Greska("Ne možete obrisati rezisera koji ima filmove.");

            var reziser = await _context.Reziseri.FindAsync(id);
            if (reziser == null) return ServiceResult.Greska("Režiser nije pronađen.");

            _context.Reziseri.Remove(reziser);
            await _context.SaveChangesAsync();
            return ServiceResult.Ok();
        }
    }
}
