namespace Backend.Models.DTO
{
    public record UpitDTOInsertUpdate
    
        (
        int PasSifra,
        int UdomiteljSifra,
        DateOnly DatumUpita,
        string StatusUpita,
        string Napomene
    );
    
}
