using System.ComponentModel.DataAnnotations;

namespace praktika1.Models
{
    public class LoginValidationModel
    {
        [Required(ErrorMessage = "Korisničko ime ili email je obavezno.")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna.")]
        public string Password { get; set; }
    }
}
