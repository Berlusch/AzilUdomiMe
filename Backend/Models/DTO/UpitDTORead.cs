namespace Backend.Models.DTO
{
    /// <summary>
    /// DTO za čitanje podataka o upitu.
    /// </summary>
    /// <param name="Sifra">Jedinstveni identifikator upita.</param>
    /// <param name="PasIme">Ime psa.</param>
    /// <param name="UdomiteljImePrezime">Ime i prezime udomitelja.</param>
    /// <param name="DatumUpita">Datum upita.</param>
    /// <param name="StatusUpita">Status upita.</param>
    /// <param name="SadrzajUpita">Sadržaj upita.</param>
    public record UpitDTORead
    (
        int Sifra,
        string PasIme, 
        string UdomiteljImePrezime, 
        DateTime DatumUpita, 
        string StatusUpita,
        string SadrzajUpita
    );
}
