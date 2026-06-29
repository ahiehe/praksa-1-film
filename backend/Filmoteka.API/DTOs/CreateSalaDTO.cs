using Filmoteka.API.Models;

namespace Filmoteka.API.DTOs
{
    public class CreateSalaDTO
    {
        public string Naziv { get; set; }
        public int Kapacitet { get; set; }
        public TipSale Tip { get; set; }
    }
}
