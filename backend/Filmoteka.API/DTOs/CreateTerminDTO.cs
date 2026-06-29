using System.ComponentModel.DataAnnotations;

namespace Filmoteka.API.DTOs
{
    public class CreateTerminDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Sala je obavezna.")]
        public int SalaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Film je obavezan.")]
        public int FilmId { get; set; }

        [Required(ErrorMessage = "Početak projekcije je obavezan.")]
        public DateTime PocetakProjekcije { get; set; }

        [Required(ErrorMessage = "Kraj projekcije je obavezan.")]
        public DateTime KrajProjekcije { get; set; }
    }
}
