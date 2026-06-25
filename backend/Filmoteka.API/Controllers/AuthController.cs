using Filmoteka.API.DTOs;
using MainProjectOOPIII3.Services;
using MainProjectOOPIII3.Services.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;
using praktika1.Models;

namespace praktika1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService AuthService)
        {
            _authService = AuthService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterValidationModel podatke)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult<AuthResponseDTO> result = await _authService.RegisterAsync(podatke);

            if (!result.Uspesno)
            {
                return BadRequest(new { message = result.Poruka });
            }

            return Ok(result.Podaci);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginValidationModel podatke)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult<AuthResponseDTO> result = await _authService.LoginAsync(podatke);

            if (!result.Uspesno)
            {
                return BadRequest(new { message = result.Poruka });
            }

            return Ok(result.Podaci);
        }
    }
}
