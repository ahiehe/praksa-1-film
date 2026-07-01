using Filmoteka.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Filmoteka.API.DTOs
{
    public class CreateSalaDTO
    {
        [Required(ErrorMessage = "Naziv je obavezan.")]
        [StringLength(100, ErrorMessage = "Naziv ne sme biti duži od 100 karaktera.")]
        public string Naziv { get; set; }

        [Range(1, 10000, ErrorMessage = "Kapacitet mora biti između 1 i 10000.")]
        public int Kapacitet { get; set; }

        [EnumDataType(typeof(TipSale), ErrorMessage = "Nevalidan tip sale.")]
        public TipSale Tip { get; set; }
    }
}
