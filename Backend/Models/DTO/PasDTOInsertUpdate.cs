using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
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
    );


}