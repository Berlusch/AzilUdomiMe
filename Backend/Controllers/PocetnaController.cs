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
    /// <param name="_mapper">Mapira podatke.</param>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PocetnaController(BackendContext _context, IMapper _mapper) : ControllerBase
    {
        /// <summary>
        /// Dohvaća psa po šifri.
        /// </summary>
        /// <param>sifra</param>
        /// <returns>Dohvaća podatke o psu po šifri.</returns>
        [HttpGet]
        [Route("pasPoSifri/{sifra:int}")]
                public ActionResult<PasDTORead> GetPasPoSifri(int sifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            Pas? e;
            try
            {
                e = _context.Psi.Include(g => g.Status).FirstOrDefault(g => g.Sifra == sifra);
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }
            if (e == null)
            {
                return NotFound(new { poruka = "Pas ne postoji u bazi" });
            }

            return Ok(_mapper.Map<PasDTORead>(e));
        }



        /// <summary>
        /// Traži pse na određenoj lokaciji sa statusom "slobodan" ili "privremeni smještaj" te ih prikazuje s paginacijom.
        /// </summary>
        /// <param name="stranica">Broj stranice (počinje od 1).</param>
        /// <param name="grad">Opcionalni parametar za filtriranje po gradu.</param>
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
                var query = _context.Psi
                .Include(p => p.Status)
                //.Include(p => p.Lokacija)
                .Where(p => p.Status.Naziv == "slobodan"
                         || p.Status.Naziv == "privremeni smještaj"
                         || p.Status.Naziv == "na liječenju");

                /*if (!string.IsNullOrEmpty(grad))
                {
                    query = query.Where(p => p.Lokacija.Grad == grad);
                }*/

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
        /// Omogućuje upis podataka o upitu putem obrasca kojeg korisnik ispunjava. 
        /// </summary>        /// 
        /// <returns>Status kreiranja upita.</returns>
        [HttpPost]
        [Route("upitObrazac")]
        public IActionResult Post([FromBody] UpitObrazacDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { poruka = ModelState });

            try
            {
                
                bool isDuplicate = _context.Upiti
                    .Any(u => u.SadrzajUpita == dto.SadrzajUpita && u.Pas.Sifra == dto.PasSifra && u.Udomitelj.Email == dto.Email);

                if (isDuplicate)
                    return BadRequest(new { poruka = "Taj upit je već poslan." });

                var udomitelj = _context.Udomitelji.FirstOrDefault(u => u.Email == dto.Email);
                if (udomitelj == null)
                {
                    udomitelj = new Udomitelj
                    {
                        Ime = dto.Ime,
                        Prezime = dto.Prezime,
                        Email = dto.Email,
                        Adresa = dto.Adresa,
                        Telefon = dto.Telefon
                    };
                    _context.Udomitelji.Add(udomitelj);
                    _context.SaveChanges();
                }

                var pas = _context.Psi.FirstOrDefault(p => p.Sifra == dto.PasSifra);
                if (pas == null)
                    return NotFound(new { poruka = "Pas nije pronađen." });

                var upit = _mapper.Map<Upit>(dto);
                upit.Pas = pas;
                upit.Udomitelj = udomitelj;

                _context.Upiti.Add(upit);
                _context.SaveChanges();

                return Ok(new { poruka = "Upit je uspješno kreiran." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { poruka = ex.Message });
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
                
                return BadRequest(e.Message);
            }
        }
    }
}
