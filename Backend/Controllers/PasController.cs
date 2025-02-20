using AutoMapper;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


namespace Backend.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class PasController(BackendContext context, IMapper mapper) : BackendController(context, mapper)
    {


        // RUTE
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


        [HttpGet]
        [Route("{sifra:int}")]
        public ActionResult<PasDTORead> GetBySifra(int sifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
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
                return NotFound(new { poruka = "Pas ne postoji u bazi" });
            }

            return Ok(_mapper.Map<PasDTORead>(e));
        }

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
                // 📌 Prvo pronaći postojećeg psa
                var pas = _context.Psi.Find(sifra);
                if (pas == null)
                {
                    return NotFound(new { poruka = "Pas ne postoji u bazi" });
                }

                // 🔄 Ažurirati svojstva
                _mapper.Map(dto, pas); // Mapira DTO u postojeći objekt
                pas.Status = s;

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
    }
}




