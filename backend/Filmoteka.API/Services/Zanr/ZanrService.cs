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
            var Zanr = new praktika1.Models.Zanr
            {
                Naziv = dto.Naziv
            };
            _context.Zanrovi.Add(Zanr);
            await _context.SaveChangesAsync();
            return ServiceResult<int>.Ok(Zanr.Id);
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
            var Zanr = await _context.Zanrovi.FindAsync(id);
            if (Zanr == null) return ServiceResult.Greska("Zanr nije pronađen.");

            _context.Zanrovi.Remove(Zanr);
            await _context.SaveChangesAsync();
            return ServiceResult.Ok();
        }
    }
}
