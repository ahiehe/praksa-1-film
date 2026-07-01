using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;

namespace Filmoteka.API.Services.Zanr
{
    public class ZanrService : IZanrService
    {
        private readonly MyAppContext _context;
        public ZanrService(MyAppContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<List<praktika1.Models.Zanr>>> GetAllAsync()
        {
            var zanrovi = await _context.Zanrovi.ToListAsync();
            return ServiceResult<List<praktika1.Models.Zanr>>.Ok(zanrovi);
        }

        public async Task<ServiceResult<int>> CreateAsync(CreateZanrDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Naziv))
            {
                return ServiceResult<int>.Greska("Naziv žanra je obavezan.");
            }

            if (await _context.Zanrovi.AnyAsync(z => z.Naziv == dto.Naziv))
            {
                return ServiceResult<int>.Greska("Žanr sa ovim nazivom već postoji.");
            }

            var Zanr = new praktika1.Models.Zanr
            {
                Naziv = dto.Naziv
            };
            _context.Zanrovi.Add(Zanr);
            await _context.SaveChangesAsync();
            return ServiceResult<int>.Ok(Zanr.Id, "Žanr je uspešno kreiran.");
        }

        public async Task<ServiceResult> UpdateAsync(int id, CreateZanrDTO dto)
        {
            var Zanr = await _context.Zanrovi.FindAsync(id);
            if (Zanr == null) return ServiceResult.Greska("Zanr nije pronađen.");

            Zanr.Naziv = dto.Naziv;
            await _context.SaveChangesAsync();
            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            bool imaFilmova = await _context.Filmovi.AnyAsync(f => f.ZanrId == id);
            if (imaFilmova)
                return ServiceResult.Greska("Ne možete obrisati žanr koji ima filmove.");

            var Zanr = await _context.Zanrovi.FindAsync(id);
            if (Zanr == null) return ServiceResult.Greska("Zanr nije pronađen.");

            _context.Zanrovi.Remove(Zanr);
            await _context.SaveChangesAsync();
            return ServiceResult.Ok();
        }
    }
}
