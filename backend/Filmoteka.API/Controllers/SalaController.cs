using Filmoteka.API.DTOs;
using Filmoteka.API.Models;
using Filmoteka.API.Services.Sala;
using MainProjectOOPIII3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praktika1.Models;

namespace Filmoteka.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SalaController : ControllerBase
    {
        private readonly ISalaService _salaService;
        
        public SalaController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ServiceResult<List<Sala>> result = await _salaService.GetAllAsync();
            return Ok(result.Podaci);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ServiceResult<Sala> result = await _salaService.GetByIdAsync(id);
            if (!result.Uspesno)
            {
                return NotFound(new { message = result.Poruka });
            }
            return Ok(result.Podaci);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateSalaDTO sala)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult<int> result = await _salaService.CreateAsync(sala);
            if (!result.Uspesno)
            {
                return BadRequest(new { message = result.Poruka });
            }

            return Ok(new { id = result.Podaci });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult result = await _salaService.DeleteAsync(id);
            if (!result.Uspesno)
            {
                return NotFound(new { message = result.Poruka });
            }
            return Ok(new { message = result.Poruka });
        }


    }
}
