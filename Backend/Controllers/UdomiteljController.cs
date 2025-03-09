using AutoMapper;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>
    /// Kontroler za upravljanje udomiteljima u aplikaciji.
    /// </summary>
    /// <param name="context">Kontekst baze podataka.</param>
    /// <param name="mapper">Mapper za mapiranje objekata.</param>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UdomiteljController(BackendContext context, IMapper mapper) : BackendController(context, mapper)
    {
        /// <summary>
        /// Dohvaća sve udomitelje.
        /// </summary>
        /// <returns>Lista udomitelja.</returns>

        [HttpGet]
        public ActionResult<List<UdomiteljDTORead>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            try
            {
                return Ok(_mapper.Map<List<UdomiteljDTORead>>(_context.Udomitelji));
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }

        }

        /// <summary>
        /// Dohvaća udomitelja prema šifri.
        /// </summary>
        /// <param name="sifra">Šifra udomitelja.</param>
        /// <returns>Udomitelj.</returns>
        [HttpGet]
        [Route("{sifra:int}")]
        public ActionResult<UdomiteljDTORead> GetBySifra(int sifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            Udomitelj? e;
            try
            {
                e = _context.Udomitelji.Find(sifra);
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }
            if (e == null)
            {
                return NotFound(new { poruka = "Udomitelj ne postoji u bazi" });
            }

            return Ok(_mapper.Map<UdomiteljDTORead>(e));
        }

        /// <summary>
        /// Dodaje novog udomitelja.
        /// </summary>
        /// <param name="dto">Podaci o udomitelju.</param>
        /// <returns>Status kreiranja.</returns>
        [HttpPost]
        public IActionResult Post(UdomiteljDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            try
            {
                var e = _mapper.Map<Udomitelj>(dto);
                _context.Udomitelji.Add(e);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, _mapper.Map<UdomiteljDTORead>(e));
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }           

        }

        /// <summary>
        /// Ažurira udomitelja prema šifri.
        /// </summary>
        /// <param name="sifra">Šifra udomitelja.</param>
        /// <param name="dto">Podatci o udomitelju.</param>
        /// <returns>Status ažuriranja.</returns>

        [HttpPut]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Put(int sifra, UdomiteljDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            try
            {
                Udomitelj? e;
                try
                {
                    e = _context.Udomitelji.Find(sifra);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
                if (e == null)
                {
                    return NotFound(new { poruka = "Udomitelj ne postoji u bazi" });
                }

                e = _mapper.Map(dto, e);

                _context.Udomitelji.Update(e);
                _context.SaveChanges();

                return Ok(new { poruka = "Uspješno promijenjeno" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }

        }

        /// <summary>
        /// Briše udomitelja prema šifri.
        /// </summary>
        /// <param name="sifra">Šifra udomitelja.</param>
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
                Udomitelj? e;
                try
                {
                    e = _context.Udomitelji.Find(sifra);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
                if (e == null)
                {
                    return NotFound("Udomitelj ne postoji u bazi");
                }
                _context.Udomitelji.Remove(e);
                _context.SaveChanges();
                return Ok(new { poruka = "Uspješno obrisano" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }
        }

        /// <summary>
        /// Traži udomitelja prema uvjetu.
        /// </summary>
        /// <param name="uvjet">Uvjet</param>
        /// <returns>Traženi udomitelj</returns>

        [HttpGet]
        [Route("trazi/{uvjet}")]
        public ActionResult<List<UdomiteljDTORead>> TraziUdomitelja(string uvjet)
        {
            if (uvjet == null || uvjet.Length < 3)
            {
                return BadRequest(ModelState);
            }
            uvjet = uvjet.ToLower();
            try
            {
                IEnumerable<Udomitelj> query = _context.Udomitelji;
                var niz = uvjet.Split(" ");
                foreach (var s in uvjet.Split(" "))
                {
                    query = query.Where(u => u.Ime.ToLower().Contains(s) || u.Prezime.ToLower().Contains(s));
                }
                var udomitelji = query.ToList();
                return Ok(_mapper.Map<List<UdomiteljDTORead>>(udomitelji));
            }
            catch (Exception e)
            {
                return BadRequest(new { poruka = e.Message });
            }
        }

        /*/// <summary>
        /// Traži udomitelje s paginacijom.
        /// </summary>
        /// <param name="stranica">Broj stranice.</param>
        /// <param name="uvjet">Uvjet pretrage.</param>
        /// <returns>Lista udomitelja.</returns>
        [HttpGet]
        [Route("traziStranicenje/{stranica}")]
        public IActionResult TraziUdomiteljStranicenje(int stranica, string uvjet = "")
        {
            var poStranici = 7;
            uvjet = uvjet.ToLower();
            try
            {
                IEnumerable<Udomitelj> query = _context.Udomitelji;

                var niz = uvjet.Split(" ");
                foreach (var s in uvjet.Split(" "))
                {
                    query = query.Where(p => p.Ime.ToLower().Contains(s) || p.Prezime.ToLower().Contains(s));
                }
                query
                    .OrderBy(p => p.Prezime);
                var udomitelji = query.ToList();
                return Ok(_mapper.Map<List<UdomiteljDTORead>>(udomitelji.Skip((poStranici * stranica) - poStranici)).Take(poStranici));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/


    }
}