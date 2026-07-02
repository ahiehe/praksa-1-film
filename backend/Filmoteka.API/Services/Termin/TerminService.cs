using Filmoteka.API.DTOs;
using Filmoteka.API.Models;
using Filmoteka.API.Services.Email;
using MainProjectOOPIII3.Services;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;

namespace Filmoteka.API.Services.Termin
{
    public class TerminService : ITerminService
    {
        private readonly MyAppContext _context;
        private readonly IEmailService _emailService;
        private readonly ILogger<TerminService> _logger;

        public TerminService(MyAppContext context, IEmailService emailService, ILogger<TerminService> logger)
        {
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<ServiceResult<int>> CreateAsync(CreateTerminDTO termin)
        {
            if (_context.Salas.Find(termin.SalaId) == null)
            {
                return ServiceResult<int>.Greska("Sala sa datim ID-jem ne postoji.");
            }
            if (_context.Filmovi.Find(termin.FilmId) == null)
            {
                return ServiceResult<int>.Greska("Film sa datim ID-jem ne postoji.");
            }
            if (termin.PocetakProjekcije >= termin.KrajProjekcije)
            {
                return ServiceResult<int>.Greska("Pocetak projekcije mora biti pre kraja projekcije.");
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

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var termin = await _context.Termini
                .Include(t => t.Film)
                .Include(t => t.Sala)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (termin == null)
                return ServiceResult.Greska("Termin nije pronađen.");

            var rezervacije = await _context.Rezervacije
                .Include(r => r.User)
                .Where(r => r.TerminId == id)
                .ToListAsync();

            foreach (var rezervacija in rezervacije)
            {
                try
                {
                    await _emailService.SendAsync(
                        rezervacija.User.Email,
                        $"Otkazana projekcija: {termin.Film.Naziv}",
                        $@"
                            <h2>Obaveštenje o otkazanoj projekciji</h2>
                            <p>Poštovani {rezervacija.User.Username},</p>
                            <p>Nažalost, projekcija na kojoj ste imali rezervaciju je otkazana:</p>
                            <ul>
                                <li><strong>Film:</strong> {termin.Film.Naziv}</li>
                                <li><strong>Datum i vreme:</strong> {termin.PocetakProjekcije:dd.MM.yyyy HH:mm}</li>
                                <li><strong>Sala:</strong> {termin.Sala.Naziv}</li>
                            </ul>
                            <p>Izvinjavamo se na neprijatnosti.</p>
                        "
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Greška pri slanju emaila korisniku {rezervacija.User.Email}");
                }
            }

            _context.Termini.Remove(termin);
            await _context.SaveChangesAsync();
            return ServiceResult.Ok("Termin uspešno obrisan.");
        }

        public async Task<ServiceResult<TerminDetailsDTO>> GetDetailsByIdAsync(int id, int korisnikId)
        {
            var termin = await _context.Termini
                .Include(t => t.Film).ThenInclude(f => f.Zanr)
                .Include(t => t.Film).ThenInclude(f => f.Reziseri)
                .Include(t => t.Sala)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (termin == null)
            {
                return ServiceResult<TerminDetailsDTO>.Greska("Termin nije pronađen.");
            }

            bool vecRezervisan = await _context.Rezervacije.AnyAsync(r => r.TerminId == id && r.UserId == korisnikId);

            TerminDetailsDTO details =  new TerminDetailsDTO
                {
                    Id = termin.Id,
                    NazivFilma = termin.Film.Naziv,
                    NazivSale = termin.Sala.Naziv,
                    BrojDostupnihMesta = termin.BrojDostupnihMesta,
                    PocetakProjekcije = termin.PocetakProjekcije,
                    KrajProjekcije = termin.KrajProjekcije,
                    OpisFilma = termin.Film.Opis,
                    GodinaIzdanja = termin.Film.GodinaIzdanja,
                    ZanrNaziv = termin.Film.Zanr.Naziv,
                    Reziseri = termin.Film.Reziseri.Select(r => r.Ime).ToList(),
                    KapacitetSale = termin.Sala.Kapacitet,
                    VecRezervisano = vecRezervisan,
                    TipSale = termin.Sala.Tip
            };

            return ServiceResult<TerminDetailsDTO>.Ok(details);
        }

        public async Task<ServiceResult<List<TerminInfoDTO>>> GetActiveTerminiInfoAsync()
        {
            var termini = await _context.Termini
                .Select(t => new TerminInfoDTO
                {
                    Id = t.Id,
                    NazivFilma = t.Film.Naziv,
                    NazivSale = t.Sala.Naziv,
                    BrojDostupnihMesta = t.BrojDostupnihMesta,
                    PocetakProjekcije = t.PocetakProjekcije,
                    KrajProjekcije = t.KrajProjekcije,
                    TipSale = t.Sala.Tip
                })
                .Where(t => t.PocetakProjekcije > DateTime.Now && t.BrojDostupnihMesta > 0)
                .ToListAsync();

            return ServiceResult<List<TerminInfoDTO>>.Ok(termini);
        }

        public async Task<ServiceResult> UpdateAsync(int id, CreateTerminDTO termin)
        {
            var stariTermin = await _context.Termini.FindAsync(id);

            if (stariTermin == null)
            {
                return ServiceResult.Greska("Termin nije pronađen.");
            }

            if (await _context.Salas.FindAsync(termin.SalaId) == null)
            {
                return ServiceResult.Greska("Sala sa datim ID-jem ne postoji.");
            }
            if (await _context.Filmovi.FindAsync(termin.FilmId) == null)
            {
                return ServiceResult.Greska("Film sa datim ID-jem ne postoji.");
            }
            if (termin.PocetakProjekcije >= termin.KrajProjekcije)
            {
                return ServiceResult.Greska("Pocetak projekcije mora biti pre kraja projekcije.");
            }

            bool terminPostoje = await _context.Termini.AnyAsync(t => t.Id != id && t.SalaId == termin.SalaId &&
                        termin.PocetakProjekcije < t.KrajProjekcije.AddMinutes(15) &&
                        termin.KrajProjekcije > t.PocetakProjekcije.AddMinutes(-15));

            if (terminPostoje)
            {
                return ServiceResult.Greska("Termin se preklapa sa postojećim terminom u istoj sali.");
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
