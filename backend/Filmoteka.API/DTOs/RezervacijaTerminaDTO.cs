using System.ComponentModel.DataAnnotations;

namespace Filmoteka.API.DTOs
{
    public class RezervacijaTerminaDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "ID termina je obavezno")]
        public int TerminId { get; set; }
    }
}
