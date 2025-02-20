using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    public record PasDTOInsertUpdate(
        [Required(ErrorMessage = "Ime je obavezno")]
        string Ime,
        [Required(ErrorMessage = "Broj čipa je obavezan")]
        string BrojCipa,
        DateTime Datum_Rodjenja,
        [Required(ErrorMessage = "Unesite 'muški' ili 'ženski'.")]   
        string Spol,
        string Opis,
        bool Kastracija,
        int StatusSifra
    );


}