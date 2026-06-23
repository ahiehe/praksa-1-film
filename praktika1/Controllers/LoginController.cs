using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using praktika1.Data;
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
        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterValidationModel podatke)
        {
            if (!ModelState.IsValid)
            {
                return View(podatke);
            }

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
                Email = podatke.Email
                
            };

            newUser.PasswordHash = HashPassword(newUser, podatke.Password);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Home");
        }
    }
}
