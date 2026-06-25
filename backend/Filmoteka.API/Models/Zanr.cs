using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace praktika1.Models
{
    public class Zanr
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Naziv ne sme biti duze od 100 karaktera.")]
        public string Naziv { get; set; }

        [JsonIgnore]
        public List<Film> Filmovi { get; set; } = new List<Film>();
    }
}
