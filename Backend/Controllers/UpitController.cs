﻿using AutoMapper;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    /// <summary>
    /// Kontroler za upravljanje upitima u aplikaciji.
    /// </summary>
    /// <param name="context">Instanca BackendContext klase koja se koristi za pristup bazi podataka.</param>
    /// <param name="mapper">Instanca IMapper sučelja koja se koristi za mapiranje objekata.</param>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UpitController(BackendContext context, IMapper mapper) : BackendController(context, mapper)
    {
        /// <summary>
        /// Dohvaća sve upite.
        /// </summary>
        /// <returns>Lista upita.</returns>

        [HttpGet]
        public ActionResult<List<UpitDTORead>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            try
            {
                return Ok(_mapper.Map<List<UpitDTORead>>(_context.Upiti.Include(u=>u.Pas).Include(u=>u.Udomitelj)));
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }

        }

        /// <summary>
        /// Dohvaća upit prema šifri.
        /// </summary>
        /// <param name="sifra">Šifra upita.</param>
        /// <returns>Upit sa zadanom šifrom.</returns>
        [HttpGet]
        [Route("{sifra:int}")]
        public ActionResult<UpitDTOInsertUpdate> GetBySifra(int sifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            Upit? e;
            try
            {
                e = _context.Upiti.Include(g => g.Pas).Include(g=>g.Udomitelj).FirstOrDefault(g => g.Sifra == sifra);
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }
            if (e == null)
            {
                return NotFound(new { poruka = "Upit ne postoji u bazi" });
            }

            return Ok(_mapper.Map<UpitDTOInsertUpdate>(e));
        }

        /// <summary>
        /// Dodaje novi upit.
        /// </summary>
        /// <param name="dto">Podaci o upitu.</param>
        /// <returns>Status kreiranja.</returns>
        [HttpPost]
        public IActionResult Post(UpitDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }

            Pas? p;
            try
            {
                p = _context.Psi.Find(dto.PasSifra);
            }
            catch (Exception e)
            {
                return BadRequest(new { poruka = e.Message });
            }
            if (p == null)
            {
                return NotFound(new { poruka = "Pas ne postoji u bazi" });
            }

            Udomitelj? u;
            try
            {
                u = _context.Udomitelji.Find(dto.UdomiteljSifra);
            }
            catch (Exception e)
            {
                return BadRequest(new { poruka = e.Message });
            }
            if (u == null)
            {
                return NotFound(new { poruka = "Udomitelj ne postoji u bazi" });
            }


            try
            {
                var e = _mapper.Map<Upit>(dto);
                e.Pas = p;
                e.Udomitelj = u;
                _context.Upiti.Add(e);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, _mapper.Map<UpitDTORead>(e));
            }
            catch (Exception e)
            {
                return BadRequest(new { poruka = e.Message });
            }
            

        }

        /// <summary>
        /// Ažurira postojeći upit.
        /// </summary>
        /// <param name="sifra">Šifra upita.</param>
        /// <param name="dto">Podaci o upitu.</param>
        /// <returns>Status ažuriranja.</returns>

        [HttpPut]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Put(int sifra, UpitDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { poruka = ModelState });
            }
            try
            {
                Upit? e;
                try
                {
                    e = _context.Upiti.Include(g => g.Pas).Include(g => g.Udomitelj).FirstOrDefault(g => g.Sifra == sifra);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
                if (e == null)
                {
                    return NotFound(new { poruka = "Upit ne postoji u bazi" });
                }

                e = _mapper.Map(dto, e);

                Pas? p;
                try
                {
                    p = _context.Psi.Find(dto.PasSifra);
                }
                catch (Exception e1)
                {
                    return BadRequest(new { poruka = e1.Message });
                }
                if (p == null)
                {
                    return NotFound(new { poruka = "Pas ne postoji u bazi" });
                }

                Udomitelj? u;
                try
                {
                    u = _context.Udomitelji.Find(dto.UdomiteljSifra);
                }
                catch (Exception e1)
                {
                    return BadRequest(new { poruka = e1.Message });
                }
                if (u == null)
                {
                    return NotFound(new { poruka = "Udomitelj ne postoji u bazi" });
                }
                e.Pas = p; 
                e.Udomitelj = u;

                _context.Upiti.Update(e);
                _context.SaveChanges();

                return Ok(new { poruka = "Uspješno promijenjeno" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { poruka = ex.Message });
            }

        }

        /// <summary>
        /// Briše upit prema šifri.
        /// </summary>
        /// <param name="sifra">Šifra upita.</param>
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
                Upit? e;
                try
                {
                    e = _context.Upiti.Find(sifra);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { poruka = ex.Message });
                }
                if (e == null)
                {
                    return NotFound("Upit ne postoji u bazi");
                }
                _context.Upiti.Remove(e);
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

