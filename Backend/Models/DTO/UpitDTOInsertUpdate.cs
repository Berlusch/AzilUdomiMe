namespace Backend.Models.DTO
{
    public record UpitDTOInsertUpdate
    
        (
        int PasSifra,
        int UdomiteljSifra,
        DateTime DatumUpita,
        string StatusUpita,
        string Napomene
    );
    
}
