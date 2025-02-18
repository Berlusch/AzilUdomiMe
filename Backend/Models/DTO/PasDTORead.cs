namespace Backend.Models.DTO
{
    public record PasDTORead(
    string Ime,
    string BrojCipa,
    DateTime Datum_Rodjenja,
    Spol SpolVrsta,
    string Opis,
    bool Kastracija,
    StatusEnum StatusOpis
);
    public enum Spol
    {
        M, // Muški
        Ž  // Ženski
    }

    public enum StatusEnum
    {
        Udomljen,
        Rezerviran,
        Slobodan,
        PrivremeniSmjestaj
    }
}
