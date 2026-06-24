using praktika1.Models;

namespace praktika1.DTOs
{
    public class PaginatedFilmsDTO
    {
        public List<Film> Filmovi { get; set; } = new();
        public int TrenutnaStranica { get; set; }
        public int UkupnoStranica { get; set; }
    }
}
