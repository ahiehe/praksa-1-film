using Filmoteka.API.DTOs;
using Filmoteka.API.Services.User;
using MainProjectOOPIII3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Filmoteka.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ServiceResult<List<UserInfoDTO>> result = await _userService.GetAllAsync();
            return Ok(result.Podaci);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ServiceResult<UserInfoDTO> result = await _userService.GetByIdAsync(id);
            if (!result.Uspesno)
                return NotFound(new { message = result.Poruka });
            return Ok(result.Podaci);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            ServiceResult<int> result = await _userService.CreateAsync(dto);
            if (!result.Uspesno)
                return BadRequest(new { message = result.Poruka });
            return Ok(new { id = result.Podaci });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            ServiceResult result = await _userService.UpdateAsync(id, dto);
            if (!result.Uspesno)
                return NotFound(new { message = result.Poruka });
            return Ok(new { message = result.Poruka });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult result = await _userService.DeleteAsync(id);
            if (!result.Uspesno)
                return NotFound(new { message = result.Poruka });
            return Ok(new { message = result.Poruka });
        }
    }
}