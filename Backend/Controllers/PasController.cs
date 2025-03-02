using AutoMapper;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


namespace Backend.Controllers
{
    /// <summary>
    /// Kontroler za upravljanje psima.
    /// </summary>
    /// <param name="context">Kontekst baze podataka.</param>
    /// <param name="mapper">Mapper za mapiranje objekata.</param>

    [ApiController]
    [Route("api/v1/[controller]")]
    public class PasController(BackendContext context, IMapper mapper) : BackendController(context, mapper)
    {
        /// <summary>
        /// Dohvaća sve pse.
        /// </summary>
        /// <returns>Lista pasa.</returns>
        [HttpGet]
        public ActionResult<List<PasDTORead>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            try
            {
                return Ok(_mapper.Map<List<PasDTORead>>(_context.Psi.Include(p => p.Status)));
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }

        }

        /// <summary>
        /// Dohvaća psa prema šifri.
        /// </summary>
        /// <param name="sifra">Šifra psa.</param>
        /// <returns>Polaznik.</returns>
        [HttpGet]
        [Route("{sifra:int}")]
        public ActionResult<PasDTOInsertUpdate> GetBySifra(int sifra)
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

            return Ok(_mapper.Map<PasDTOInsertUpdate>(e));
        }

        /// <summary>
        /// Dodaje novog psa.
        /// </summary>
        /// <param name="dto">Podaci o psu.</param>
        /// <returns>Status kreiranja.</returns>
        [HttpPost]
        public IActionResult Post(PasDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            Status? s;
            try
            {
                s = _context.Statusi.Find(dto.StatusSifra);
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }
            if (s == null)
            {
                return NotFound(new { poruka = "Status ne postoji u bazi" });
            }
            if (dto.Spol != "muški" && dto.Spol != "ženski")
            {
                return BadRequest(new { poruka = "Spol mora biti muški ili ženski" });
            }
            try
            {
                var e = _mapper.Map<Pas>(dto);
                e.Status = s;
                _context.Psi.Add(e);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, _mapper.Map<PasDTORead>(e));
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }



        }

        /// <summary>
        /// Ažurira psa prema šifri.
        /// </summary>
        /// <param name="sifra">Šifra psa.</param>
        /// <param name="dto">Podaci o psu.</param>
        /// <returns>Status ažuriranja.</returns>
        [HttpPut]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Put(int sifra, PasDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }

            Status? s;
            try
            {
                s = _context.Statusi.Find(dto.StatusSifra);
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }
            if (s == null)
            {
                return NotFound(new { poruka = "Status ne postoji u bazi" });
            }
            if (dto.Spol != "muški" && dto.Spol != "ženski")
            {
                return BadRequest(new { poruka = "Spol mora biti muški ili ženski" });
            }

            try
            {
                var pas = _context.Psi.Find(sifra);
                if (pas == null)
                {
                    return NotFound(new { poruka = "Pas ne postoji u bazi" });
                }
                
                _mapper.Map(dto, pas); // Mapira DTO u postojeći objekt
                pas.Status = s;
                _context.Psi.Update(pas);
                _context.SaveChanges();
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    poruka = $"Uspješno promijenjeni podatci za psa {pas.Ime}"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }
        }

        /// <summary>
        /// Briše psa prema šifri.
        /// </summary>
        /// <param name="sifra">Šifra psa.</param>
        /// <returns>Status brisanja.</returns>
        [HttpDelete]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int sifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            try
            {
                Pas? e;
                try
                {
                    e = _context.Psi.Find(sifra);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
                if (e == null)
                {
                    return NotFound("Pas ne postoji u bazi");
                }
                _context.Psi.Remove(e);
                _context.SaveChanges();
                return Ok(new { poruka = $"Uspješno obrisan pas {e.Ime}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }
        }

        /// <summary>
        /// Traži pse prema uvjetu.
        /// </summary>
        /// <param name="uvjet">Uvjet pretrage.</param>
        /// <returns>Lista pasa.</returns>

        [HttpGet]
        [Route("trazi/{uvjet}")]
        public ActionResult<List<PasDTORead>> TraziPsa(string uvjet)
        {
            if (uvjet == null || uvjet.Length < 3)
            {
                return BadRequest("Uvjet mora sadržavati barem 3 slova!");
            }
            uvjet = uvjet.ToLower();
            try
            {
                IEnumerable<Pas> query = _context.Psi;
                var niz = uvjet.Split(" ");
                foreach (var s in uvjet.Split(" ")) { 
                    query = query.Where(p => p.Ime.ToLower().Contains(s));
                }               
                var psi = query.ToList();
                return Ok(_mapper.Map<List<PasDTORead>>(psi));
            }
            catch (Exception e)
            {
                return BadRequest(new { poruka = e.Message });
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
        public IActionResult TraziPasStranicenje(int stranica, string uvjet = "")
        {
            var poStranici = 7;
            uvjet = uvjet.ToLower();
            try
            {
                IEnumerable<Pas> query = _context.Psi;

                var niz = uvjet.Split(" ");
                foreach (var s in uvjet.Split(" "))
                {
                    query = query.Where(p => p.Ime.ToLower().Contains(s));
                }
                query
                    .OrderBy(p => p.Ime);
                var psi = query.ToList();
                return Ok(_mapper.Map<List<PasDTORead>>(psi.Skip((poStranici * stranica) - poStranici)).Take(poStranici));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Postavlja sliku za psa.
        /// </summary>
        /// <param name="sifra">Šifra psa.</param>
        /// <param name="slika">Podatci o slici.</param>
        /// <returns>Status postavljanja slike.</returns>
        /*[HttpPut]
        [Route("postaviSliku/{sifra:int}")]
        public IActionResult PostaviSliku(int sifra, SlikaDTO slika)
        {
            if (sifra <= 0)
            {
                return BadRequest("Šifra mora biti veća od nula (0)");
            }
            if (slika.Base64 == null || slika.Base64?.Length == 0)
            {
                return BadRequest("Slika nije postavljena");
            }
            var p = _context.Psi.Find(sifra);
            if (p == null)
            {
                return BadRequest("Ne postoji pas s šifrom " + sifra + ".");
            }
            try
            {
                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "slike" + ds + "psi");

                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                var putanja = Path.Combine(dir + ds + sifra + ".png");
                System.IO.File.WriteAllBytes(putanja, Convert.FromBase64String(slika.Base64!));
                return Ok("Uspješno pohranjena slika");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/
    }
}




