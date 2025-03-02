using Backend.Data;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Backend.Controllers
{
    /// <summary>
    /// Kontroler za početne operacije.
    /// </summary>
    /// <param name="_context">Kontekst baze podataka.</param>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PocetnaController(BackendContext _context) : ControllerBase
    {
        /// <summary>
        /// Dohvaća pse za udomljavanje.
        /// </summary>
        /// <returns>Popis pasa za udomljavanje.</returns>
        [HttpGet]
        [Route("DostupniPsi")]
        public ActionResult<List<PasDTORead>>DostupniPsi()
        {
            try
            {
                var psi = _context.Psi.ToList();
                var lista = new List<object>();
                foreach (var pas in psi)
                {
                    lista.Add(new { pas.Ime });
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }
        }
    }
}
