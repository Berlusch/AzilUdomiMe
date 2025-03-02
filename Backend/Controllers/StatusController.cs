using AutoMapper;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    /// <summary>
    /// Kontroler za upravljanje statusima.
    /// </summary>
    /// <param name="context">Kontekst baze podataka.</param>
    /// <param name="mapper">Mapper za mapiranje objekata.</param>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StatusController(BackendContext context, IMapper mapper) : BackendController(context, mapper)
    {
        /// <summary>
        /// Dohvaća sve statuse.
        /// </summary>
        /// <returns>Lista statusa.</returns>
        [HttpGet]
        public ActionResult<List<StatusDTORead>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            try
            {
                return Ok(_mapper.Map<List<StatusDTORead>>(_context.Statusi));
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }

        }

        /// <summary>
        /// Dohvaća status prema šifri.
        /// </summary>
        /// <param name="sifra">Šifra statusa.</param>
        /// <returns>Status.</returns>
        [HttpGet]
        [Route("{sifra:int}")]
        public ActionResult<StatusDTORead> GetBySifra(int sifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            Status? e;
            try
            {
                e = _context.Statusi.Find(sifra);
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }
            if (e == null)
            {
                return NotFound(new { poruka = "Status ne postoji u bazi" });
            }

            return Ok(_mapper.Map<StatusDTORead>(e));
        }
        /// <summary>
        /// Dodaje novi status.
        /// </summary>
        /// <param name="dto">Podaci o statusu.</param>
        /// <returns>Status kreiranja.</returns>
        [HttpPost]
        public IActionResult Post(StatusDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            try
            {
                var e = _mapper.Map<Status>(dto);
                _context.Statusi.Add(e);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, _mapper.Map<StatusDTORead>(e));
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }

        }

        /// <summary>
        /// Ažurira status prema šifri.
        /// </summary>
        /// <param name="sifra">Šifra statusa.</param>
        /// <param name="dto">Podaci o statusu.</param>
        /// <returns>Status ažuriranja.</returns>
        [HttpPut]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Put(int sifra, StatusDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            try
            {
                Status? e;
                try
                {
                    e = _context.Statusi.Find(sifra);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
                if (e == null)
                {
                    return NotFound(new { poruka = "Status ne postoji u bazi" });
                }

                e = _mapper.Map(dto, e);

                _context.Statusi.Update(e);
                _context.SaveChanges();

                return Ok(new { poruka = "Uspješno promijenjeno" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }

        }

        /// <summary>
        /// Briše status prema šifri.
        /// </summary>
        /// <param name="sifra">Šifra statusa.</param>
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
                Status? e;
                try
                {
                    e = _context.Statusi.Find(sifra);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
                if (e == null)
                {
                    return NotFound("Status ne postoji u bazi");
                }
                _context.Statusi.Remove(e);
                _context.SaveChanges();
                return Ok(new { poruka = "Uspješno obrisano" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }
        }


    }
}
