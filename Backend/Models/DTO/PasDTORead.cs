namespace Backend.Models.DTO
{
    /// <summary>
    /// DTO za čitanje podataka o psu.
    /// </summary>
    /// <param name="Sifra">Jedinstvena šifra psa.</param>
    /// <param name="Ime">Ime psa.</param>
    /// <param name="BrojCipa">Broj čipa psa.</param>
    /// <param name="Datum_Rodjenja">Datum rođenja psa.</param>
    /// <param name="Spol">Spol psa.</param>
    /// <param name="Opis">Opis psa.</param>
    /// <param name="Kastracija">Indikator je li pas kastriran ili ne.</param>
    /// <param name="StatusNaziv">Naziv statusa psa.</param>
    /// <param name="LokacijaGrad">Grad lokacije psa.</param>
    public record PasDTORead(
        int Sifra,
    string Ime,
    string BrojCipa,
    DateTime Datum_Rodjenja,
    string Spol,
    string Opis,
    bool Kastracija,
    string StatusNaziv
    //string LokacijaGrad
);

}
