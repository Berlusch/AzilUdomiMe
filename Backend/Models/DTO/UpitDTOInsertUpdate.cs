namespace Backend.Models.DTO
{
    public record UpitDTOInsertUpdate
    
        (
        int PasSifra,
        Pas Pas,
        int UdomiteljSifra,
        Udomitelj Udomitelj,
        DateOnly DatumUpita,
        string StatusUpita,
        string SadrzajNapomene
    );
    
}
