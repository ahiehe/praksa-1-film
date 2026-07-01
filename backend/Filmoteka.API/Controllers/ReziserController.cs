using Filmoteka.API.DTOs;
using Filmoteka.API.Services.Reziser;
using MainProjectOOPIII3.Services;
using MainProjectOOPIII3.Services.Film;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Filmoteka.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReziserController : ControllerBase
    {
        private readonly IReziserService _reziserService;
        public ReziserController(IReziserService reziserService)
        {
            _reziserService = reziserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reziserService.GetAllAsync();
            return Ok(result.Podaci);
        }

        [HttpGet("options")]
        public async Task<IActionResult> GetOptions()
        {
            ServiceResult<List<ReziserOptionDTO>> result = await _reziserService.GetOptionsAsync();
            return Ok(result.Podaci);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateReziserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _reziserService.CreateAsync(dto);
            if (!result.Uspesno)
                return BadRequest(new { message = result.Poruka });
            return Ok(new { id = result.Podaci });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateReziserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _reziserService.UpdateAsync(id, dto);
            if (!result.Uspesno) return NotFound(new { message = result.Poruka });
            return Ok(new { message = "Režiser je promenjen." });
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reziserService.DeleteAsync(id);
            if (!result.Uspesno) return NotFound(new { message = result.Poruka });
            return Ok(new { message = "Režiser je obrisan." });
        }
    }
}
