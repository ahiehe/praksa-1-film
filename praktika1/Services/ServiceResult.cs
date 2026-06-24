namespace MainProjectOOPIII3.Services
{
    public class ServiceResult
    {
        public bool Uspesno { get; init; }
        public string? Poruka { get; init; }
        public string? KljucGreske { get; init; }

        public static ServiceResult Ok(string? poruka = null)
            => new() { Uspesno = true, Poruka = poruka };

        public static ServiceResult Greska(string poruka, string? kljucGreske = null)
            => new() { Uspesno = false, Poruka = poruka, KljucGreske = kljucGreske };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Podaci { get; init; }

        public static ServiceResult<T> Ok(T podaci, string? poruka = null)
            => new() { Uspesno = true, Podaci = podaci, Poruka = poruka };
        public static new ServiceResult<T> Greska(string poruka, string? kljucGreske = null)
            => new() { Uspesno = false, Poruka = poruka, KljucGreske = kljucGreske, Podaci = default };
    }
}