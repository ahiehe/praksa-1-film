using System.ComponentModel.DataAnnotations;

namespace praktika1.Models
{
    public enum Role
    {
        Admin,
        Radnik,
        User
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username je obavezno polje.")]
        [StringLength(30, ErrorMessage = "Username ne sme biti duze od 30 karaktera.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Email je obavezno polje.")]
        [EmailAddress(ErrorMessage = "Neispravan format email adrese.")]
        [StringLength(50, ErrorMessage = "Email ne moze biti duzi od 50 karaktera")]
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public Role Role { get; set; }
    }

}