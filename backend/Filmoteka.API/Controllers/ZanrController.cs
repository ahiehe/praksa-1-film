using Filmoteka.API.DTOs;
using Filmoteka.API.Services.Zanr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Filmoteka.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZanrController : ControllerBase
    {
        private readonly IZanrService _ZanrService;
        public ZanrController(IZanrService ZanrService)
        {
            _ZanrService = ZanrService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ZanrService.GetAllAsync();
            return Ok(result.Podaci);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateZanrDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _ZanrService.CreateAsync(dto);
            return Ok(new { id = result.Podaci });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateZanrDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _ZanrService.UpdateAsync(id, dto);
            if (!result.Uspesno) return NotFound(new { message = result.Poruka });
            return Ok(new { message = "Zanr je promenjen." });
        }

        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ZanrService.DeleteAsync(id);
            if (!result.Uspesno) return NotFound(new { message = result.Poruka });
            return Ok(new { message = "Zanr je obrisan." });
        }
    }
}
