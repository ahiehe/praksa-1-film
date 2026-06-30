using System.ComponentModel.DataAnnotations;

namespace Filmoteka.API.DTOs
{
    public class CreateFilmDTO
    {
        [Required(ErrorMessage = "Naziv je obavezan.")]
        [StringLength(250, ErrorMessage = "Naziv ne sme biti duži od 250 karaktera.")]
        public string Naziv { get; set; }

        [Range(1888, 2100, ErrorMessage = "Godina izdanja nije validna.")]
        public int GodinaIzdanja { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Žanr je obavezan.")]
        public int ZanrId { get; set; }

        [StringLength(2500, ErrorMessage = "Opis ne sme biti duži od 2500 karaktera.")]
        public string? Opis { get; set; }

        [MinLength(1, ErrorMessage = "Morate izabrati barem jednog režisera.")]
        public int[] IzabraniReziseri { get; set; }

        public DateTime? PocetakPrikazivanja { get; set; }
        public DateTime? KrajPrikazivanja { get; set; }
    }
}
