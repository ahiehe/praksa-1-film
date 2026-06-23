using System.ComponentModel.DataAnnotations;

namespace praktika1.Models
{
    public class Reziser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Ime ne sme biti duze od 50 karaktera.")]
        public string Ime { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Ime ne sme biti duze od 50 karaktera.")]
        public string Prezime { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public List<Film> Filmovi { get; set; }
    }
}
