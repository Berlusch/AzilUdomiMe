using System.ComponentModel.DataAnnotations;
namespace Backend.Models.DTO
{
    /// <summary>    
    /// Ovaj DTO se koristi za čitanje i unos podataka iz obrasca upita u bazu
    /// </summary>  
    /// <param name="Ime">Ime udomitelja.</param>
    /// <param name="Prezime">Prezime udomitelja.</param>
    /// <param name="Email">Email udomitelja.</param>
    /// <param name="SadrzajUpita">Sadržaj upita.</param>
    /// <param name="PasSifra">Šifra psa.</param>
    /// <param name="Adresa">Adresa udomitelja.</param>
    /// <param name="Telefon">Telefon udomitelja (format: +385 XX XXXXXXX)</param>
    public record UpitObrazacDTO
    (
        int PasSifra,
        string Ime,
        string Prezime,
        [EmailAddress(ErrorMessage = "Email nije ispravan")]
        string Email,
        string SadrzajUpita,
        [Required(ErrorMessage = "Telefon obavezan")]
        [RegularExpression(@"^\+385\d{1,2}\d{6,8}$",
            ErrorMessage = "Telefon mora biti u formatu +385 XX XXXXXXX (samo hrvatski brojevi)")]
        string Telefon,
        string Adresa
    );
}