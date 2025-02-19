namespace Backend.Models.DTO
{
    public record PasDTORead(
        int Sifra,
    string Ime,
    string BrojCipa,
    DateTime Datum_Rodjenja,
    string Spol,
    string Opis,
    bool Kastracija,
    string StatusNaziv
);
    
}
