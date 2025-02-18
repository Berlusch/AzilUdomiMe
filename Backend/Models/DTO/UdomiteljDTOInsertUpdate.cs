using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    public record UdomiteljDTOInsertUpdate(
        [Required(ErrorMessage = "Ime obavezno")]
        string Ime,
        [Required(ErrorMessage = "Prezime obavezno")]
        string Prezime,
        [Required(ErrorMessage = "Adresa obavezna")]
        string Adresa,
        [Required(ErrorMessage = "Telefon obavezan")]
        string Telefon,
        [Required(ErrorMessage = "Email obavezan")]
        string Email
        );
}
