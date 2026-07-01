using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace praktika1.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int GodinaIzdanja { get; set; }

        public int ZanrId { get; set; }

        [ForeignKey("ZanrId")]
        public Zanr? Zanr { get; set; }

        public List<Reziser>? Reziseri { get; set; }
        public DateTime? PocetakPrikazivanja { get; set; }
        public DateTime? KrajPrikazivanja { get; set; }
    }
}
