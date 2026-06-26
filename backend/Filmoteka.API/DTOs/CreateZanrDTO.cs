using System.ComponentModel.DataAnnotations;

namespace Filmoteka.API.DTOs
{
    public class CreateZanrDTO
    {
        [Required]
        public string Naziv { get; set; }
    }
}
