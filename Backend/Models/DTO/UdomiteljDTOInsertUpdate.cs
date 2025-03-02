using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    /// <summary>
    /// DTO za unos i ažuriranje udomitelja.
    /// </summary>
    /// <param name="Ime">Ime udomitelja (obavezno)</param>
    /// <param name="Prezime">Prezime udomitelja (obavezno)</param>
    /// <param name="Adresa">Adresa udomitelja (obavezno)</param>
    /// <param name="Telefon">Telefon udomitelja (obavezno)</param>
    /// <param name="Email">Email udomitelja (obavezno, u ispravnom formatu)</param>
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
        [EmailAddress(ErrorMessage = "Email nije ispravan")]
        string Email
        );
}
