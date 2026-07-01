using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;

namespace Filmoteka.API.Services.Sala
{
    public class SalaService : ISalaService
    {
        private readonly MyAppContext _context;

        public SalaService(MyAppContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<int>> CreateAsync(CreateSalaDTO sala)
        {
            if (_context.Salas.Any(s => s.Naziv == sala.Naziv))
            {
                return ServiceResult<int>.Greska("Sala sa ovim nazivom već postoji.");
            }

            Models.Sala newSala = new Models.Sala
            {
                Naziv = sala.Naziv,
                Kapacitet = sala.Kapacitet,
                Tip = sala.Tip
            };

            _context.Add(newSala);
            await _context.SaveChangesAsync();

            return ServiceResult<int>.Ok(newSala.Id, "Sala je uspešno kreirana.");
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var sala = await _context.Salas.FindAsync(id);

            if (sala == null)
            {
                return ServiceResult.Greska("Sala sa ovim ID-jem ne postoji.");
            }

            var termin = await _context.Termini.FirstOrDefaultAsync(t => t.SalaId == id);

            if (termin != null)
            {
                return ServiceResult.Greska("Sala se ne može obrisati jer je povezana sa terminom.");
            }

            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();
            return ServiceResult.Ok("Sala je uspešno obrisana.");
        }

        public async Task<ServiceResult<List<Models.Sala>>> GetAllAsync()
        {
            var sale = await _context.Salas.ToListAsync();
            return ServiceResult<List<Models.Sala>>.Ok(sale);
        }

        public async Task<ServiceResult<Models.Sala>> GetByIdAsync(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            return ServiceResult<Models.Sala>.Ok(sala);
        }

    }
}
