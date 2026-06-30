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

        public async Task<ServiceResult<PaginatedFilmsDTO>> GetPaginatedFilmsAsync(FilmQueryDTO filters, int pageSize)
        {
            var query = _context.Filmovi
                .Include(f => f.Zanr)
                .Include(f => f.Reziseri)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filters.Search))
            {
                query = query.Where(f => f.Naziv.Contains(filters.Search) || f.Opis.Contains(filters.Search));
            }

            if (filters.UBioskopima.HasValue && filters.UBioskopima.Value)
            {
                query = query.Where(f => f.PocetakPrikazivanja != null &&
                                            f.KrajPrikazivanja != null && 
                                            f.PocetakPrikazivanja <= DateTime.Now && 
                                            f.KrajPrikazivanja >= DateTime.Now);
            }

            if (filters.GodinaOd.HasValue)
            {
                query = query.Where(f => f.GodinaIzdanja >= filters.GodinaOd.Value);
            }

            if (filters.GodinaDo.HasValue)
            {
                query = query.Where(f => f.GodinaIzdanja <= filters.GodinaDo.Value);
            }

            if (filters.ZanrId.HasValue)
            {
                query = query.Where(f => f.ZanrId == filters.ZanrId.Value);
            }

            if (filters.Page < 1) filters.Page = 1;

            var ukupnoFilmova = await query.CountAsync();

            int ukupnoStranica = (int)Math.Ceiling((double)ukupnoFilmova / pageSize);

            ukupnoStranica = ukupnoStranica == 0 ? 1 : ukupnoStranica;

            if (filters.Page > ukupnoStranica && ukupnoStranica > 0) filters.Page = ukupnoStranica;


            var filmovi = await query
                .OrderBy(f => f.Id)
                .Skip((filters.Page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return ServiceResult<PaginatedFilmsDTO>.Ok(new PaginatedFilmsDTO
            {
                Filmovi = filmovi,
                TrenutnaStranica = filters.Page,
                UkupnoStranica = ukupnoStranica
            });
        }

        public async Task<ServiceResult<praktika1.Models.Film>> GetFilmByIdAsync(int id)
        {
            var film = await _context.Filmovi
               .Include(f => f.Zanr)
               .Include(f => f.Reziseri)
               .FirstOrDefaultAsync(f => f.Id == id);
            if (film == null)
            {
                return ServiceResult<praktika1.Models.Film>.Greska("Film ne postoje");
            }
            return ServiceResult<praktika1.Models.Film>.Ok(film);
        }

        public async Task<ServiceResult<int>> CreateFilmAsync(CreateFilmDTO dto)
        {

            if (!_context.Zanrovi.Any(z => z.Id == dto.ZanrId))
            {
                return ServiceResult<int>.Greska("Izabrani žanr ne postoji!");
            }

            if (dto.PocetakPrikazivanja.HasValue && dto.KrajPrikazivanja.HasValue && dto.PocetakPrikazivanja > dto.KrajPrikazivanja)
            {
                return ServiceResult<int>.Greska("Datum početka prikazivanja ne može biti veći od datuma kraja prikazivanja!");
            }

            if (dto.IzabraniReziseri == null || dto.IzabraniReziseri.Length == 0)
            {
                return ServiceResult<int>.Greska("Morate izabrati barem jednog režisera!");
            }

            var film = new praktika1.Models.Film
            {
                Naziv = dto.Naziv,
                GodinaIzdanja = dto.GodinaIzdanja,
                ZanrId = dto.ZanrId,
                Opis = dto.Opis,
                PocetakPrikazivanja = dto.PocetakPrikazivanja,
                KrajPrikazivanja = dto.KrajPrikazivanja
            };

            film.Reziseri = await _context.Reziseri
                    .Where(r => dto.IzabraniReziseri.Contains(r.Id))
                    .ToListAsync();

            _context.Add(film);
            await _context.SaveChangesAsync();
            return ServiceResult<int>.Ok(film.Id);
        }

        public async Task<ServiceResult> UpdateFilmAsync(int id, CreateFilmDTO dto)
        {
            if (!_context.Zanrovi.Any(z => z.Id == dto.ZanrId))
            {
                return ServiceResult.Greska("Izabrani žanr ne postoji!");
            }

            if (dto.PocetakPrikazivanja.HasValue && dto.KrajPrikazivanja.HasValue && dto.PocetakPrikazivanja > dto.KrajPrikazivanja)
            {
                return ServiceResult.Greska("Datum početka prikazivanja ne može biti veći od datuma kraja prikazivanja!");
            }

            if (dto.IzabraniReziseri == null || dto.IzabraniReziseri.Length == 0)
            {
                return ServiceResult.Greska("Morate izabrati barem jednog režisera!");
            }

            var filmIzBaze = await _context.Filmovi
                .Include(f => f.Reziseri)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (filmIzBaze == null) return ServiceResult.Greska("Film nije pronadjen");

            filmIzBaze.Naziv = dto.Naziv;
            filmIzBaze.GodinaIzdanja = dto.GodinaIzdanja;
            filmIzBaze.ZanrId = dto.ZanrId;
            filmIzBaze.Opis = dto.Opis;
            filmIzBaze.PocetakPrikazivanja = dto.PocetakPrikazivanja;
            filmIzBaze.KrajPrikazivanja = dto.KrajPrikazivanja;

            filmIzBaze.Reziseri.Clear();
            var noviReziseri = await _context.Reziseri
                .Where(r => dto.IzabraniReziseri.Contains(r.Id))
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
