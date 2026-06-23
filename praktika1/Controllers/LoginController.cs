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
        private MyAppContext _context;
        public LoginController(MyAppContext context)
        {
            _context = context;
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
            bool usernameExists = await _context.Users.AnyAsync(u => u.Username == podatke.Username);
            if (usernameExists)
            {
                ModelState.AddModelError("Username", "Korisničko ime je već zauzeto.");
            }

            bool emailExists = await _context.Users.AnyAsync(u => u.Email == podatke.Email);
            if (emailExists)
            {
                ModelState.AddModelError("Email", "Email adresa je već zauzeta.");
            }

            if (!ModelState.IsValid)
            {
                return View(podatke);
            }

            User newUser = new User
            {
                Username = podatke.Username,
                Email = podatke.Email,
                Role = Role.User
            };

            newUser.PasswordHash = HashPassword(podatke.Password);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();


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

            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == podatke.UsernameOrEmail || u.Email == podatke.UsernameOrEmail);

            if (user == null || !VerifyPassword(user.PasswordHash, podatke.Password))
            {
                ModelState.AddModelError("Password", "Neispravno ime/email ili lozinka.");
                return View(podatke);
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role.ToString());

            return RedirectToAction("Index", "Home");
        }
    }
}
