using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UdomiteljController : ControllerBase
    {
        private readonly BackendContext _context;

        public UdomiteljController(BackendContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_context.Udomitelji);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }
        [HttpGet]
        [Route("{sifra:int}")]
        public IActionResult GetBySifra(int sifra)
        {
            try
            {
                var s = _context.Udomitelji.Find(sifra);
                if (s == null)
                {
                    return NotFound();
                }
                return Ok(s);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpPost]
        public IActionResult Post(Udomitelj udomitelj)
        {

            try
            {
                _context.Udomitelji.Add(udomitelj);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, udomitelj);



            }
            catch (Exception e)
            {

                return BadRequest(e);
            }




        }

        [HttpPut]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Put(int sifra, Udomitelj udomitelj)

        {
            try
            {

                var s = _context.Udomitelji.Find(sifra);
                if (s == null)
                {
                    return NotFound();
                }
                //rucno mapiranje, kasnije automaper
                s.Ime = udomitelj.Ime;
                s.Prezime = udomitelj.Prezime;
                s.Adresa = udomitelj.Adresa;
                s.Telefon = udomitelj.Telefon;
                s.Email = udomitelj.Email;
                _context.Udomitelji.Update(s);
                _context.SaveChanges();
                return Ok(new { poruka = "Uspješno ažurirano" });
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }

        [HttpDelete]
        [Route("{sifra:int}")]
        public IActionResult Delete(int sifra)
        {
            try
            {
                var s = _context.Udomitelji.Find(sifra);
                if (s == null)
                {
                    return NotFound();
                }
                _context.Udomitelji.Remove(s);
                _context.SaveChanges();
                return Ok(new { poruka = "Uspješno obrisano" });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }




    }
}
