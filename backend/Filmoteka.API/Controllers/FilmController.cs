using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;
using MainProjectOOPIII3.Services.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using praktika1.DTOs;
using praktika1.Models;


namespace praktika1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {

        private IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int page = 1)
        {
            ServiceResult<PaginatedFilmsDTO> result = await _filmService.GetPaginatedFilmsAsync(page, 6);

            return Ok(result.Podaci);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> DetailsFilm(int id)
        {
            ServiceResult<Film> result = await _filmService.GetFilmByIdAsync(id);
            Film film = result.Podaci;

            if (film == null)
            {
                return NotFound();
            }

            return Ok(film);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFilm([FromBody] CreateFilmDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var film = new Film
            {
                Naziv = dto.Naziv,
                GodinaIzdanja = dto.GodinaIzdanja,
                ZanrId = dto.ZanrId,
                Opis = dto.Opis
            };


            ServiceResult result = await _filmService.CreateFilmAsync(film, dto.IzabraniReziseri);
            if (result.Uspesno)
            {
                return Ok( new { id = film.Id });
            }

            return BadRequest(new { message = result.Poruka });
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateFilm(int id, [FromBody] CreateFilmDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var film = new Film
            {
                Naziv = dto.Naziv,
                GodinaIzdanja = dto.GodinaIzdanja,
                ZanrId = dto.ZanrId,
                Opis = dto.Opis
            };

            ServiceResult result = await _filmService.UpdateFilmAsync(id, film, dto.IzabraniReziseri);

            if (result.Poruka == "notfound")
            {
                return NotFound();
            }

            if (result.Uspesno)
            {
                return Ok(new { message = "Film je promenjen" });
            }

            return BadRequest(new { message = result.Poruka });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            
            await _filmService.DeleteFilmAsync(id);

            return Ok(new { message = "Film je obrisan" });
        }

        [HttpGet("zanrovi")]
        public async Task<IActionResult> GetZanrovi()
        {
            var result = await _filmService.GetZanroviAsync();

            return Ok(result.Podaci);
        }

        [HttpGet("reziseri")]
        public async Task<IActionResult> GetReziseri()
        {
            var result = await _filmService.GetReziseriAsync();
            return Ok(result.Podaci);
        }

    }
}
