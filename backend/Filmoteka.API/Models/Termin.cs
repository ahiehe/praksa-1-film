using praktika1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Filmoteka.API.Models
{
    public class Termin
    {
        [Key]
        public int Id { get; set; }

        public int FilmId { get; set; }
        [ForeignKey("FilmId")]
        public Film Film { get; set; }

        public int SalaId { get; set; }
        [ForeignKey("SalaId")]
        public Sala Sala { get; set; }

        public DateTime PocetakProjekcije { get; set; }

        public DateTime KrajProjekcije { get; set; }

        public int BrojDostupnihMesta { get; set; }
    }
}
