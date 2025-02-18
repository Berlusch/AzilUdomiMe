namespace Backend.Models.DTO
{
    public record PasDTORead(
    string Ime,
    string BrojCipa,
    string Pasmina,
    DateTime Datum_Rodjenja,
    Spol SpolVrsta,
    Velicina VelicinaPsa,
    Boja BojaPsa,
    string MojaPrica,
    bool Kastracija,
    StatusEnum StatusOpis
);

    public enum Spol
    {
        M, // Muški
        Ž  // Ženski
    }

    public enum Velicina
    {
        Mali,
        Srednji,
        Veliki
    }

    public enum Boja
    {
        Bijeli,
        Crni,
        Smeđi,
        Šareni
    }

    public enum StatusEnum
    {
        Udomljen,
        Rezerviran,
        Slobodan,
        PrivremeniSmjestaj
    }
}
