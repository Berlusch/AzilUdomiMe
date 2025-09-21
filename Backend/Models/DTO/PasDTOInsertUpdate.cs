using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    /// <summary>
    /// DTO za unos i ažuriranje podataka o psu.
    /// </summary>
    /// <param name="Ime">Ime psa (obavezno).</param>
    /// <param name="BrojCipa">Broj čipa psa (obavezno, započinje s HR i ima 15 znamenki).</param>
    /// <param name="Datum_Rodjenja">Datum rođenja psa.</param>
    /// <param name="Spol">Spol psa (muški ili ženski).</param>
    /// <param name="Opis">Opis psa.</param>
    /// <param name="Kastracija">Indikator je li pas kastriran ili ne.</param>
    /// <param name="StatusSifra">Status psa.</param>
    ///// <param name="LokacijaSifra">Lokacija psa.</param>


    public record PasDTOInsertUpdate(
        [Required(ErrorMessage = "Ime je obavezno")]
        string Ime,
        [Required(ErrorMessage = "Broj čipa je obavezan")]
        [RegularExpression(@"^HR\d{15}$", ErrorMessage = "Broj čipa započinje s 'HR' i sadrži 15 znamenki.")]
        string BrojCipa,
        DateTime Datum_Rodjenja,
        [Required(ErrorMessage = "Unesite 'muški' ili 'ženski'.")]   
        string Spol,
        string Opis,
        bool Kastracija,
        int StatusSifra
        //int LokacijaSifra 

    );


}