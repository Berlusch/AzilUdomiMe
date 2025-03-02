namespace Backend.Models.DTO
{
    /// <summary>
    /// DTO za čitanje podataka o statusu.
    /// </summary>
    /// <param name="Sifra">Jedinstvena šifra statusa.</param>
    /// <param name="Naziv">Naziv statusa.</param>
    public record StatusDTORead(
        int Sifra,
        string Naziv  
    );
}
