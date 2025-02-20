namespace Backend.Models.DTO
{
    public record UpitDTORead
    (
        int Sifra,
        string PasIme, 
        string UdomiteljImePrezime, 
        DateTime DatumUpita, 
        string StatusUpita,
        string Napomene 
    );
}
