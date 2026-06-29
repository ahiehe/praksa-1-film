using praktika1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Filmoteka.API.Models
{

    namespace Filmoteka.API.Models
    {
        public class Rezervacija
        {
            [Key]
            public int Id { get; set; }
            public int TerminId { get; set; }
            [ForeignKey("TerminId")]
            public Termin Termin { get; set; }

            public int UserId { get; set; }
            [ForeignKey("UserId")]
            public User User { get; set; }
        }
    }
}
