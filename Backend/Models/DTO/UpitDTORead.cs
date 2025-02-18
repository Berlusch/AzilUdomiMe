namespace Backend.Models.DTO
{
    public record UpitDTORead
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
