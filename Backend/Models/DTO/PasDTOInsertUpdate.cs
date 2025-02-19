using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    public record PasDTOInsertUpdate(
        [Required(ErrorMessage = "Ime obavezno")]
        string Ime,
        [Required(ErrorMessage = "Broj čipa obavezan")]
        string BrojCipa,
        DateTime Datum_Rodjenja,
        Spol Spol,
        string Opis,
        bool Kastracija,
        Status StatusOpis,
        int? UdomiteljSifra
    );

    
}