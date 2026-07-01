using System.ComponentModel.DataAnnotations;

namespace Filmoteka.API.Models
{
    public enum TipSale
    {
        Standard,
        IMAX,
        _3D
    }
    public class Sala
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int Kapacitet { get; set; }
        public TipSale Tip { get; set; }
        
    }
}
