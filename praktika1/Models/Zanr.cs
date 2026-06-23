using System.ComponentModel.DataAnnotations;

namespace praktika1.Models
{
    public class Zanr
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }
        public List<Film> Filmovi { get; set; } = new List<Film>();
    }
}
