using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    public record StatusDTOInsertUpdate(
        [Required(ErrorMessage = "Status obavezan!")]
        [RegularExpression(@"^(slobodan|udomljen|rezerviran|privremeni smještaj)$", 
            ErrorMessage = "Status mora biti jedan od sljedećih: slobodan, udomljen, rezerviran, privremeni smještaj.")]
        string Naziv
    );
}
