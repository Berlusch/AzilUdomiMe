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
        /// Dohvaća sve pse iz baze.
        /// </summary>
        /// <returns>Popis svih pasa u bazi.</returns>
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
        /// Traži pse sa statusom "slobodan" ili "privremeni smještaj" te ih prikazuje s paginacijom.
        /// </summary>
        /// <param name="stranica">Broj stranice (počinje od 1).</param>
        /// <returns>Objekt koji sadrži listu pasa s traženim statusom.</returns>
        [HttpGet]
        [Route("traziStranicenje/{stranica}")]
        public IActionResult TraziStranicenje(int stranica)
        {
            if (stranica < 1)
            {
                return BadRequest("Broj stranice mora biti 1 ili veći.");
            }

            var poStranici = 4;
            try
            {
                var query = _context.Psi.Include(p => p.Status)
                    .Where(p => p.Status.Naziv == "slobodan" || p.Status.Naziv == "privremeni smještaj");

                

                var psi = query.OrderBy(p => p.Ime)
                    .Skip((stranica - 1) * poStranici)
                    .Take(poStranici)
                    .ToList();

                if (!psi.Any())
                {
                    return Ok(new
                    {
                        Poruka = "Trenutačno nema pasa sa statusom 'slobodan' ili 'privremeni smještaj'.",
                        Psi = new List<PasDTORead>(),
                        
                        
                    });
                }

                return Ok(new
                {
                    Psi = _mapper.Map<List<PasDTORead>>(psi),
                    
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Traži broj pasa sa statusom "slobodan" ili "privremeni smještaj" te računa ukupan broj stranica na kojima će se prikazati.
        /// </summary>        /// 
        /// <returns>Ukupan broj stranica na kojima će se prikazati psi sa traženim statusom.</returns>
        [HttpGet]
        [Route("izracunajUkupnoStranica/")]
        public IActionResult IzracunajUkupnoStranica()
        {
            var poStranici = 4;
            try
            {var query = _context.Psi.Include(p => p.Status)
                    .Where(p => p.Status.Naziv == "slobodan" || p.Status.Naziv == "privremeni smještaj");

                var ukupniBrojPasa = query.Count();
                var ukupnoStranica = (int)Math.Ceiling((double)ukupniBrojPasa / poStranici);


                return Ok(ukupnoStranica);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>
        /// Traži udomljene pse te utvrđuje njihov broj.
        /// </summary>
        /// <returns>Broj udomljenih pasa.</returns>
        [HttpGet]
        [Route("traziUdomljenePse")]
        public IActionResult TraziUdomljenePse()
        {
            try
            {                
                IEnumerable<Pas> query = _context.Psi.Include(p => p.Status);

                query = query.Where(p => p.Status.Naziv == "udomljen");

                int brojUdomljenihPasa = query.Count();
                                
                return Ok(brojUdomljenihPasa);
            }
            catch (Exception e)
            {
                // Ako dođe do greške, vraćamo BadRequest sa porukom greške
                return BadRequest(e.Message);
            }
        }
    }
}
