namespace Backend.Models.DTO
{
    /// <summary>
    /// DTO za čitanje podataka o udomitelju.
    /// </summary>
    /// <param name="Sifra">Jedinstvena šifra udomitelja.</param>
    /// <param name="Ime">Ime udomitelja.</param>
    /// <param name="Prezime">Prezime udomitelja.</param>
    /// <param name="Adresa">Adresa udomitelja.</param>
    /// <param name="Telefon">Telefon udomitelja.</param>
    /// <param name="Email">Email udomitelja.</param>
    public record UdomiteljDTORead(
        int Sifra,
        string Ime,
        string Prezime,
        string Adresa,
        string Telefon,
        string Email
        );


}
