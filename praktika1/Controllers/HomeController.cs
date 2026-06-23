using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;
using praktika1.Filters;
using praktika1.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace praktika1.Controllers
{
    public class HomeController : Controller
    {

        private MyAppContext _context;

        public HomeController(MyAppContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) page = 1;

            int pageSize = 6; 
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

            ViewBag.TrenutnaStranica = page;
            ViewBag.UkupnoStranica = ukupnoStranica;

            return View(filmovi);
        }

        [AdminRequired]
        public async Task<IActionResult> CreateFilm()
        {

            var zanrovi = await _context.Zanrovi.ToListAsync();
            ViewData["ZanrId"] = new SelectList(zanrovi, "Id", "Naziv");

            var reziseri = await _context.Reziseri.Select(r => new { r.Id, ImePrezime = r.Ime + " " + r.Prezime }).ToListAsync();
            ViewBag.ReziseriId = new MultiSelectList(reziseri, "Id", "ImePrezime");

            return View();
        }

        [HttpPost]
        [AdminRequired]
        public async Task<IActionResult> CreateFilm([Bind("Naziv,GodinaIzdanja,ZanrId,Opis")] Film film, int[] izabraniReziseri)
        {
            

            if (izabraniReziseri == null || izabraniReziseri.Length == 0)
            {
                ModelState.AddModelError("izabraniReziseri", "Morate izabrati barem jednog režisera!");
            }

            if (ModelState.IsValid)
            {
                film.Reziseri = await _context.Reziseri
                    .Where(r => izabraniReziseri.Contains(r.Id))
                    .ToListAsync();  

                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var zanrovi = await _context.Zanrovi.ToListAsync();
            ViewData["ZanrId"] = new SelectList(zanrovi, "Id", "Naziv", film.ZanrId);

            var reziseri = await _context.Reziseri.Select(r => new { r.Id, ImePrezime = r.Ime + " " + r.Prezime }).ToListAsync();
            ViewBag.ReziseriId = new MultiSelectList(reziseri, "Id", "ImePrezime");

            return View(film);
        }

        public async Task<IActionResult> DetailsFilm(int id)
        {
            var film = await _context.Filmovi
                .Include(f => f.Zanr)
                .Include(f => f.Reziseri)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        [AdminRequired]
        public async Task<IActionResult> UpdateFilm(int id)
        {
            

            var film = await _context.Filmovi
                   .Include(f => f.Zanr)
                   .Include(f => f.Reziseri)
                   .FirstOrDefaultAsync(f => f.Id == id);

            if (film == null)
            {
                return NotFound();
            }

            var zanrovi = await _context.Zanrovi.ToListAsync();
            ViewBag.ZanrId = new SelectList(zanrovi, "Id", "Naziv", film.ZanrId);

            var trenutniReziseriIds = film.Reziseri.Select(r => r.Id).ToArray();
            var reziseri = await _context.Reziseri
                .Select(r => new { r.Id, ImePrezime = r.Ime + " " + r.Prezime })
                .ToListAsync();
            ViewBag.ReziseriId = new MultiSelectList(reziseri, "Id", "ImePrezime", trenutniReziseriIds);

            return View(film);
        }


        [HttpPost]
        [AdminRequired]
        public async Task<IActionResult> UpdateFilm(int id, [Bind("Id,Naziv,GodinaIzdanja,ZanrId,Opis")] Film film, int[] izabraniReziseri)
        {

            if (id != film.Id) return NotFound();

            if (izabraniReziseri == null || izabraniReziseri.Length == 0)
            {
                ModelState.AddModelError("izabraniReziseri", "Morate izabrati barem jednog režisera!");
            }

            if (ModelState.IsValid)
            {
                var filmIzBaze = await _context.Filmovi
                    .Include(f => f.Reziseri)
                    .Include(f => f.Zanr)
                    .FirstOrDefaultAsync(f => f.Id == id);

                if (filmIzBaze == null) return NotFound();

                filmIzBaze.Naziv = film.Naziv;
                filmIzBaze.GodinaIzdanja = film.GodinaIzdanja;
                filmIzBaze.ZanrId = film.ZanrId;
                filmIzBaze.Opis = film.Opis;

                
                
                if (izabraniReziseri != null)
                {
                    filmIzBaze.Reziseri.Clear();
                    var noviReziseri = await _context.Reziseri
                        .Where(r => izabraniReziseri.Contains(r.Id))
                        .ToListAsync();

                    foreach (var reziser in noviReziseri)
                    {
                        filmIzBaze.Reziseri.Add(reziser);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            var zanrovi = await _context.Zanrovi.ToListAsync();
            ViewBag.ZanrId = new SelectList(zanrovi, "Id", "Naziv", film.ZanrId);

            var reziseri = await _context.Reziseri.Select(r => new { r.Id, ImePrezime = r.Ime + " " + r.Prezime }).ToListAsync();
            ViewBag.ReziseriId = new MultiSelectList(reziseri, "Id", "ImePrezime", izabraniReziseri);

            return View(film);
        }

        [HttpPost]
        [AdminRequired]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            
            var film = await _context.Filmovi.FindAsync(id);

            if (film != null)
            {
                _context.Filmovi.Remove(film);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }




    }
}
