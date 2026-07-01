using Filmoteka.API.Models;

namespace Filmoteka.API.DTOs
{
    public class TerminDetailsDTO
    {
        public int Id { get; set; }
        public string NazivFilma { get; set; }
        public string OpisFilma { get; set; }
        public int GodinaIzdanja { get; set; }
        public string ZanrNaziv { get; set; }
        public List<string> Reziseri { get; set; }
        public string NazivSale { get; set; }
        public TipSale TipSale { get; set; }
        public int KapacitetSale { get; set; }
        public int BrojDostupnihMesta { get; set; }
        public DateTime PocetakProjekcije { get; set; }
        public DateTime KrajProjekcije { get; set; }

        public bool VecRezervisano { get; set; }
    }
}
