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

        private void SetUserSession(User user)
        {
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role.ToString());
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
            return RedirectToAction("Index", "Film");
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

            SetUserSession(newUser);

            return RedirectToAction("Index", "Film");
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

            SetUserSession(user);

            return RedirectToAction("Index", "Film");
        }
    }
}
