using AutoMapper;
using Backend.Data;
using Backend.Models;
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
    public class PocetnaController(BackendContext _context, IMapper _mapper) : ControllerBase
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



        /// <summary>
        /// Traži pse s paginacijom.
        /// </summary>
        /// <param name="stranica">Broj stranice.</param>
        /// <param name="uvjet">Uvjet pretrage.</param>
        /// <returns>Lista pasa.</returns>
        [HttpGet]
        [Route("traziStranicenje/{stranica}")]
        public IActionResult TraziPasStranicenje(int stranica)
        {
            var poStranici = 5;
            try
            {
                IEnumerable<Pas> query = _context.Psi.Include(p=>p.Status);
                query = query.Where(p=>p.Status.Naziv=="slobodan" || p.Status.Naziv=="privremeni smještaj");

                query.OrderBy(p => p.Ime);
                var psi = query.ToList();
                return Ok(_mapper.Map<List<PasDTORead>>(psi.Skip((poStranici * stranica) - poStranici)).Take(poStranici).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
