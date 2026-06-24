using MainProjectOOPIII3.Services;
using MainProjectOOPIII3.Services.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using praktika1.DTOs;
using praktika1.Filters;
using praktika1.Models;


namespace praktika1.Controllers
{
    public class FilmController : Controller
    {

        private IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }


        public async Task<IActionResult> Index(int page = 1)
        {
            ServiceResult<PaginatedFilmsDTO> result = await _filmService.GetPaginatedFilmsAsync(page, 6);


            ViewBag.TrenutnaStranica = result.Podaci.TrenutnaStranica;
            ViewBag.UkupnoStranica = result.Podaci.UkupnoStranica;

            return View(result.Podaci.Filmovi);
        }

        [AdminRequired]
        public async Task<IActionResult> CreateFilm()
        {
            ServiceResult<SelectList> zanroviResult = await _filmService.GetZanroviSelectListAsync();
            ServiceResult<MultiSelectList> reziseriResult = await _filmService.GetReziseriMultiSelectListAsync();

            ViewData["ZanrId"] = zanroviResult.Podaci;
            ViewBag.ReziseriId = reziseriResult.Podaci;

            return View();
        }

        [HttpPost]
        [AdminRequired]
        public async Task<IActionResult> CreateFilm([Bind("Naziv,GodinaIzdanja,ZanrId,Opis")] Film film, int[] izabraniReziseri)
        {

            if (ModelState.IsValid)
            {
                ServiceResult result = await _filmService.CreateFilmAsync(film, izabraniReziseri);
                if (result.Uspesno)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(result.KljucGreske, result.Poruka);
            }

            ServiceResult<SelectList> zanroviResult = await _filmService.GetZanroviSelectListAsync(film.ZanrId);
            ServiceResult<MultiSelectList> reziseriResult = await _filmService.GetReziseriMultiSelectListAsync(izabraniReziseri);

            ViewData["ZanrId"] = zanroviResult.Podaci;
            ViewBag.ReziseriId = reziseriResult.Podaci;

            return View(film);
        }

        public async Task<IActionResult> DetailsFilm(int id)
        {
            ServiceResult<Film> result = await _filmService.GetFilmByIdAsync(id);
            Film film = result.Podaci;

            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        [AdminRequired]
        public async Task<IActionResult> UpdateFilm(int id)
        {
            
            ServiceResult<Film> result = await _filmService.GetFilmByIdAsync(id);
            Film film = result.Podaci;

            if (film == null)
            {
                return NotFound();
            }

            var trenutniReziseriIds = film.Reziseri.Select(r => r.Id).ToArray();

            ServiceResult<SelectList> zanroviResult = await _filmService.GetZanroviSelectListAsync(film.ZanrId);
            ServiceResult<MultiSelectList> reziseriResult = await _filmService.GetReziseriMultiSelectListAsync(trenutniReziseriIds);

            ViewData["ZanrId"] = zanroviResult.Podaci;
            ViewBag.ReziseriId = reziseriResult.Podaci;

            return View(film);
        }


        [HttpPost]
        [AdminRequired]
        public async Task<IActionResult> UpdateFilm(int id, [Bind("Id,Naziv,GodinaIzdanja,ZanrId,Opis")] Film film, int[] izabraniReziseri)
        {

            if (ModelState.IsValid)
            {
                ServiceResult result = await _filmService.UpdateFilmAsync(id, film, izabraniReziseri);

                if (result.Poruka == "notfound")
                {
                    return NotFound();
                }

                if (result.Uspesno)
                {    
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(result.KljucGreske, result.Poruka);
            }

            ServiceResult<SelectList> zanroviResult = await _filmService.GetZanroviSelectListAsync(film.ZanrId);
            ServiceResult<MultiSelectList> reziseriResult = await _filmService.GetReziseriMultiSelectListAsync(izabraniReziseri);

            ViewData["ZanrId"] = zanroviResult.Podaci;
            ViewBag.ReziseriId = reziseriResult.Podaci;

            return View(film);
        }

        [HttpPost]
        [AdminRequired]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            
            await _filmService.DeleteFilmAsync(id);

            return RedirectToAction("Index");
        }




    }
}
