using Filmoteka.API.DTOs;
using Filmoteka.API.Services.Termin;
using MainProjectOOPIII3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Filmoteka.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TerminController : ControllerBase
    {
        private readonly ITerminService _terminService;
        public TerminController(ITerminService terminService)
        {
            _terminService = terminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTermini()
        {
            ServiceResult<List<TerminInfoDTO>> result = await _terminService.GetTerminiInfoAsync();
            return Ok(result.Podaci);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTerminById(int id)
        {
            ServiceResult<TerminInfoDTO> result = await _terminService.GetTerminInfoByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result.Podaci);
        }

        [HttpPost("reserve")]
        [Authorize(Roles = "User, Employee, Admin")]
        public async Task<IActionResult> ReserveTermin([FromBody] RezervacijaTerminaDTO dto)
        {
            string userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized(new { message = "Korisnik nije autentifikovan." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult result = await _terminService.RezervisiAsync(dto.TerminId, userId);
            if (!result.Uspesno)
            {
                return BadRequest(new { message = result.Poruka });
            }
            return Ok(new { message = result.Poruka });
        }

        [HttpPost("create")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<IActionResult> CreateTermin([FromBody] CreateTerminDTO terminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceResult<int> result = await _terminService.CreateTerminAsync(terminDto);
            if (!result.Uspesno)
            {
                return BadRequest(new { message = result.Poruka });
            }
            return Ok(new { id = result.Podaci });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<IActionResult> UpdateTermin(int id, [FromBody] CreateTerminDTO terminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult result = await _terminService.UpdateTerminAsync(id, terminDto);

            if (!result.Uspesno)
            {
                return NotFound(new { message = result.Poruka });
            }
            return Ok(new { message = result.Poruka });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employee, Admin")]
        public async Task<IActionResult> DeleteTermin(int id)
        {
            ServiceResult result = await _terminService.DeleteTerminAsync(id);
            if (!result.Uspesno)
            {
                return NotFound(new { message = result.Poruka });
            }

            return Ok(new { message = "Termin uspešno obrisan." });
        }
    }
}
