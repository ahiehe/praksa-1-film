namespace Filmoteka.API.DTOs
{
    public class FilmQueryDTO
    {
        public int Page { get; set; }
        public string? Search { get; set; }
        public bool? UBioskopima { get; set; }
        public int? GodinaOd { get; set; }
        public int? GodinaDo { get; set; }
        public int? ZanrId { get; set; }

    }
}
