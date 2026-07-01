using System.ComponentModel.DataAnnotations;

namespace Filmoteka.API.DTOs
{
    public class CreateZanrDTO
    {
        [Required(ErrorMessage = "Naziv je obavezan")]
        public string Naziv { get; set; }
    }
}
