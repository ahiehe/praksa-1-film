using Filmoteka.API.DTOs;
using Filmoteka.API.Models.Filmoteka.API.Models;
using MainProjectOOPIII3.Services;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;

namespace Filmoteka.API.Services.Termin
{
    public class TerminService : ITerminService
    {
        private readonly MyAppContext _context;

        public TerminService(MyAppContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<int>> CreateTerminAsync(CreateTerminDTO termin)
        {
            if (_context.Salas.Find(termin.SalaId) == null)
            {
                return ServiceResult<int>.Greska("Sala sa datim ID-jem ne postoji.");
            }
            if (_context.Filmovi.Find(termin.FilmId) == null)
            {
                return ServiceResult<int>.Greska("Film sa datim ID-jem ne postoji.");
            }

            bool terminPostoje = _context.Termini.Any(t => t.SalaId == termin.SalaId &&
                termin.PocetakProjekcije < t.KrajProjekcije.AddMinutes(15) &&
                termin.KrajProjekcije > t.PocetakProjekcije.AddMinutes(-15));

            if (terminPostoje)
            {
                return ServiceResult<int>.Greska("Termin se preklapa sa postojećim terminom u istoj sali.");
            }

            var noviTermin = new Models.Termin
            {
                SalaId = termin.SalaId,
                FilmId = termin.FilmId,
                PocetakProjekcije = termin.PocetakProjekcije,
                KrajProjekcije = termin.KrajProjekcije,
                BrojDostupnihMesta = _context.Salas.Where(s => s.Id == termin.SalaId).Select(s => s.Kapacitet).FirstOrDefault()
            };

            _context.Termini.Add(noviTermin);
            await _context.SaveChangesAsync();

            return ServiceResult<int>.Ok(noviTermin.Id, "Termin uspešno kreiran.");
        }

        public async Task<ServiceResult> DeleteTerminAsync(int id)
        {
            var termin = await _context.Termini.FindAsync(id);
            if (termin == null)
            {
                return ServiceResult.Greska("Termin nije pronađen.");
            }

            _context.Termini.Remove(termin);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok("Termin uspešno obrisan.");
        }

        public async Task<ServiceResult<TerminInfoDTO>> GetTerminInfoByIdAsync(int id)
        {
            var termin = await _context.Termini
                .Where(t => t.Id == id)
                .Select(t => new TerminInfoDTO
                {
                    Id = t.Id,
                    NazivFilma = t.Film.Naziv,
                    NazivSale = t.Sala.Naziv,
                    BrojDostupnihMesta = t.BrojDostupnihMesta,
                    PocetakProjekcije = t.PocetakProjekcije,
                    KrajProjekcije = t.KrajProjekcije
                })
                .FirstOrDefaultAsync();

            return ServiceResult<TerminInfoDTO>.Ok(termin);
        }

        public async Task<ServiceResult<List<TerminInfoDTO>>> GetTerminiInfoAsync()
        {
            var termini = await _context.Termini
                .Select(t => new TerminInfoDTO
                {
                    Id = t.Id,
                    NazivFilma = t.Film.Naziv,
                    NazivSale = t.Sala.Naziv,
                    BrojDostupnihMesta = t.BrojDostupnihMesta,
                    PocetakProjekcije = t.PocetakProjekcije,
                    KrajProjekcije = t.KrajProjekcije
                })
                .ToListAsync();

            return ServiceResult<List<TerminInfoDTO>>.Ok(termini);
        }

        public async Task<ServiceResult> UpdateTerminAsync(int id, CreateTerminDTO termin)
        {
            var stariTermin = await _context.Termini.FindAsync(id);

            if (stariTermin == null)
            {
                return ServiceResult.Greska("Termin nije pronađen.");
            }

            if (_context.Salas.Find(termin.SalaId) == null)
            {
                return ServiceResult<int>.Greska("Sala sa datim ID-jem ne postoji.");
            }
            if (_context.Filmovi.Find(termin.FilmId) == null)
            {
                return ServiceResult<int>.Greska("Film sa datim ID-jem ne postoji.");
            }

            bool terminPostoje = _context.Termini.Any(t => t.SalaId == termin.SalaId &&
                termin.PocetakProjekcije < t.KrajProjekcije.AddMinutes(15) &&
                termin.KrajProjekcije > t.PocetakProjekcije.AddMinutes(-15));

            if (terminPostoje)
            {
                return ServiceResult<int>.Greska("Termin se preklapa sa postojećim terminom u istoj sali.");
            }

            stariTermin.SalaId = termin.SalaId;
            stariTermin.FilmId = termin.FilmId;
            stariTermin.PocetakProjekcije = termin.PocetakProjekcije;
            stariTermin.KrajProjekcije = termin.KrajProjekcije;

            await _context.SaveChangesAsync();

            return ServiceResult.Ok("Termin uspešno ažuriran.");
        }

        public async Task<ServiceResult> RezervisiAsync(int terminId, int korisnikId)
        {
            var termin = await _context.Termini.FindAsync(terminId);

            if (termin == null)
            {
                return ServiceResult.Greska("Termin nije pronađen.");
            }

            bool vecRezervisan = await _context.Rezervacije.AnyAsync(r => r.TerminId == terminId && r.UserId == korisnikId);
            if (vecRezervisan)
            {
                return ServiceResult.Greska("Korisnik je već rezervisao ovaj termin.");
            }

            if (termin.BrojDostupnihMesta <= 0)
            {
                return ServiceResult.Greska("Nema dostupnih mesta za ovaj termin.");
            }

            termin.BrojDostupnihMesta--;
            var rezervacija = new Rezervacija
            {
                TerminId = terminId,
                UserId = korisnikId
            };

            _context.Rezervacije.Add(rezervacija);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok("Rezervacija uspešna.");
        }
    }
}
