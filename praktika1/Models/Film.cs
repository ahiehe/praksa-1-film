using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace praktika1.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Film mora imati naziv")]
        [StringLength(250)]
        public string Naziv { get; set; }

        [StringLength(2500, ErrorMessage = "Opis ne sme biti duze od 2500 karaktera.")]
        public string Opis { get; set; }
        public int GodinaIzdanja { get; set; }

        public int ZanrId { get; set; }

        [ForeignKey("ZanrId")]
        public Zanr? Zanr { get; set; }

        public List<Reziser>? Reziseri { get; set; } 
    }
}
