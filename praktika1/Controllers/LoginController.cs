using MainProjectOOPIII3.Services;
using MainProjectOOPIII3.Services.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;
using praktika1.Filters;
using praktika1.Models;

namespace praktika1.Controllers
{
    public class LoginController : Controller
    {
        private IAuthService _authService;
        public LoginController(IAuthService AuthService)
        {
            _authService = AuthService;
        }

        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        private bool VerifyPassword(string hash, string password)
        {
            return _passwordHasher.VerifyHashedPassword(null, hash, password) == PasswordVerificationResult.Success;
        }

        [NotLogedInRequired]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [NotLogedInRequired]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterValidationModel podatke)
        {
            if (!ModelState.IsValid)
            {
                return View(podatke);
            }

            ServiceResult<User> result = await _authService.RegisterAsync(podatke);

            if (!result.Uspesno)
            {
                ModelState.AddModelError(result.KljucGreske, result.Poruka);
                return View(podatke);
            }

            User newUser = result.Podaci;

            HttpContext.Session.SetInt32("UserId", newUser.Id);
            HttpContext.Session.SetString("Username", newUser.Username);
            HttpContext.Session.SetString("Role", newUser.Role.ToString());


            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginValidationModel podatke)
        {
            if (!ModelState.IsValid)
            {
                return View(podatke);
            }

            ServiceResult<User> result = await _authService.LoginAsync(podatke);

            if (!result.Uspesno)
            {
                ModelState.AddModelError(result.KljucGreske, result.Poruka);
                return View(podatke);
            }

            User user = result.Podaci;

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role.ToString());

            return RedirectToAction("Index", "Home");
        }
    }
}
