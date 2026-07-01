using System.ComponentModel.DataAnnotations;

namespace Filmoteka.API.DTOs
{
    public class CreateReziserDTO
    {
        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(50, ErrorMessage = "Ime ne sme biti duže od 50 karaktera.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(50, ErrorMessage = "Prezime ne sme biti duže od 50 karaktera.")]
        public string Prezime { get; set; }

        public DateTime? DatumRodjenja { get; set; }
    }
}
