namespace Backend.Models.DTO
{
    /// <summary>    
    /// Ovaj DTO se koristi za čitanje i unos podataka iz obrasca upita u bazu
    /// </summary>  
    /// <param name="Ime">Ime udomitelja.</param>
    /// <param name="Prezime">Prezime udomitelja.</param>
    /// <param name="Email">Email udomitelja.</param>
    /// <param name="SadrzajUpita">Sadržaj upita.</param>
    /// <param name="PasSifra">Šifra psa.</param>

    public record UpitObrazacDTO
    (
        int PasSifra,
        string Ime,
        string Prezime,
        string Email,
        string SadrzajUpita
    );
}
