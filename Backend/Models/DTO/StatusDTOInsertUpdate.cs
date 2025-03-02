using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    /// <summary>
    /// DTO za unos i ažuriranje statusa.
    /// </summary>
    /// <param name="Naziv">Naziv statusa</param>
    
    public record StatusDTOInsertUpdate(
          
        string Naziv
            );
}
