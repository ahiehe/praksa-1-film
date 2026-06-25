using Filmoteka.API.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;
using praktika1.DTOs;
using praktika1.Models;

namespace MainProjectOOPIII3.Services.Film
{
    public class FilmService : IFilmService
    {
        private MyAppContext _context;
        public FilmService(MyAppContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<List<Zanr>>> GetZanroviAsync()
        {
            var zanrovi = await _context.Zanrovi.ToListAsync();
            return ServiceResult<List<Zanr>>.Ok(zanrovi);
        }

        public async Task<ServiceResult<List<ReziserDTO>>> GetReziseriAsync()
        {
            var reziseri = await _context.Reziseri
                 .Select(r => new ReziserDTO { Id = r.Id, Ime = r.Ime, Prezime = r.Prezime })
                 .ToListAsync();
            return ServiceResult<List<ReziserDTO>>.Ok(reziseri);
        }

        public async Task<ServiceResult<PaginatedFilmsDTO>> GetPaginatedFilmsAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;

            var ukupnoFilmova = await _context.Filmovi.CountAsync();

            int ukupnoStranica = (int)Math.Ceiling((double)ukupnoFilmova / pageSize);

            if (page > ukupnoStranica && ukupnoStranica > 0) page = ukupnoStranica;


            var filmovi = await _context.Filmovi
                .Include(f => f.Zanr)
                .Include(f => f.Reziseri)
                .OrderBy(f => f.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return ServiceResult<PaginatedFilmsDTO>.Ok(new PaginatedFilmsDTO
            {
                Filmovi = filmovi,
                TrenutnaStranica = page,
                UkupnoStranica = ukupnoStranica
            });
        }

        public async Task<ServiceResult<praktika1.Models.Film>> GetFilmByIdAsync(int id)
        {
            return ServiceResult<praktika1.Models.Film>.Ok(await _context.Filmovi
               .Include(f => f.Zanr)
               .Include(f => f.Reziseri)
               .FirstOrDefaultAsync(f => f.Id == id));
        }

        public async Task<ServiceResult<int>> CreateFilmAsync(praktika1.Models.Film film, int[] izabraniReziseri)
        {
            if (izabraniReziseri == null || izabraniReziseri.Length == 0)
            {
                return ServiceResult<int>.Greska("Morate izabrati barem jednog režisera!", "izabraniReziseri");
            }

            film.Reziseri = await _context.Reziseri
                    .Where(r => izabraniReziseri.Contains(r.Id))
                    .ToListAsync();

            _context.Add(film);
            await _context.SaveChangesAsync();
            return ServiceResult<int>.Ok(film.Id);
        }

        public async Task<ServiceResult> UpdateFilmAsync(int id, praktika1.Models.Film film, int[] izabraniReziseri)
        {

            if (izabraniReziseri == null || izabraniReziseri.Length == 0)
            {
                return ServiceResult.Greska("Morate izabrati barem jednog režisera!", "izabraniReziseri");
            }

            var filmIzBaze = await _context.Filmovi
                .Include(f => f.Reziseri)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (filmIzBaze == null) return ServiceResult.Greska("notfound");

            filmIzBaze.Naziv = film.Naziv;
            filmIzBaze.GodinaIzdanja = film.GodinaIzdanja;
            filmIzBaze.ZanrId = film.ZanrId;
            filmIzBaze.Opis = film.Opis;

            filmIzBaze.Reziseri.Clear();
            var noviReziseri = await _context.Reziseri
                .Where(r => izabraniReziseri.Contains(r.Id))
                .ToListAsync();

            foreach (var reziser in noviReziseri)
            {
                filmIzBaze.Reziseri.Add(reziser);
            }

            await _context.SaveChangesAsync();
            return ServiceResult.Ok();
        }
        public async Task<ServiceResult> DeleteFilmAsync(int id)
        {
            var film = await _context.Filmovi.FindAsync(id);

            if (film == null)
            {
                return ServiceResult.Greska("Film nije pronadjen");
            }

            _context.Filmovi.Remove(film);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok();
        }
    }
}
