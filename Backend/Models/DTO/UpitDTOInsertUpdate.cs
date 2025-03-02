namespace Backend.Models.DTO
{
    /// <summary>
    /// DTO za unos i ažuriranje upita.
    /// </summary>
    /// <param name="PasSifra">Šifra psa.</param>
    /// <param name="UdomiteljSifra">Šifra udomitelja 
    /// <param name="DatumUpita">Datum upita.</param>
    /// <param name="StatusUpita">Status upita</param>
    /// <param name="Napomene">Napomene.</param>"
    public record UpitDTOInsertUpdate
    
        (
        int PasSifra,
        int UdomiteljSifra,
        DateTime DatumUpita,
        string StatusUpita,
        string Napomene
    );
    
}
