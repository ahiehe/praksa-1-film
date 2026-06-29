
namespace Filmoteka.API.DTOs
{
    public class TerminInfoDTO
    {
        public int Id { get; set; }
        public string NazivFilma { get; set; }
        public string NazivSale { get; set; }
        public int BrojDostupnihMesta { get; set; }
        public DateTime PocetakProjekcije { get; set; }
        public DateTime KrajProjekcije { get; set; }
    }
}
