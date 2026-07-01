using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;
using MainProjectOOPIII3.Services.Account;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Index([FromQuery] FilmQueryDTO query)
        {
            ServiceResult<PaginatedFilmsDTO> result = await _filmService.GetPaginatedFilmsAsync(query, 9);

            return Ok(result.Podaci);
        }

        [HttpGet("options")]
        public async Task<IActionResult> GetFilmOptions()
        {
            ServiceResult<List<FilmOptionDTO>> result = await _filmService.GetOptionsAsync();
            return Ok(result.Podaci);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> DetailsFilm(int id)
        {
            ServiceResult<Film> result = await _filmService.GetByIdAsync(id);

            if (!result.Uspesno)
            {
                return NotFound();
            }

            return Ok(result.Podaci);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateFilm([FromBody] CreateFilmDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult<int> result = await _filmService.CreateAsync(dto);
            if (result.Uspesno)
            {
                return Ok( new { id = result.Podaci });
            }

            return BadRequest(new { message = result.Poruka });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateFilm(int id, [FromBody] CreateFilmDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            ServiceResult result = await _filmService.UpdateAsync(id, dto);

            if (result.Uspesno)
            {
                return Ok(new { message = "Film je promenjen" });
            }

            return BadRequest(new { message = result.Poruka });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            ServiceResult result = await _filmService.DeleteAsync(id);

            if (!result.Uspesno)
            {
                return NotFound(new { message = result.Poruka });
            }

            return Ok(new { message = "Film je obrisan" });
        }
    }
}
