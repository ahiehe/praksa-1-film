namespace Filmoteka.API.DTOs
{
    public class CreateFilmDTO
    {
        public string Naziv { get; set; }
        public int GodinaIzdanja { get; set; }
        public int ZanrId { get; set; }
        public string? Opis { get; set; }
        public int[] IzabraniReziseri { get; set; }
    }
}
