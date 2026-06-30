using System.ComponentModel.DataAnnotations;

namespace Filmoteka.API.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Korisničko ime ili email je obavezno.")]
        public string UsernameOrEmail { get; set; }
        [Required(ErrorMessage = "Lozinka je obavezna.")]
        public string Password { get; set; }
    }
}
