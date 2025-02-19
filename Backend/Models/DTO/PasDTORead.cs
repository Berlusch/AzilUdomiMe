namespace Backend.Models.DTO
{
    public record PasDTORead(
    string Ime,
    string BrojCipa,
    DateTime Datum_Rodjenja,
    Spol Spol,
    string Opis,
    bool Kastracija,
    Status StatusOpis
);
    
}
