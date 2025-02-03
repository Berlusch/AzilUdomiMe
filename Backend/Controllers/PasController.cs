using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PasController : ControllerBase
    {
        private readonly BackendContext _context;

        public PasController(BackendContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_context.Psi);
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
                var s = _context.Psi.Find(sifra);
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
        public IActionResult Post(Pas pas)
        {

            try
            {
                _context.Psi.Add(pas);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, pas);



            }
            catch (Exception e)
            {

                return BadRequest(e);
            }




        }

        [HttpPut]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Put(int sifra, Pas pas)

        {
            try
            {

                var s = _context.Psi.Find(sifra);
                if (s == null)
                {
                    return NotFound();
                }
                //rucno mapiranje, kasnije automaper
                s.BrojCipa = pas.BrojCipa;
                s.Ime = pas.Ime;
                s.Datum_Rodjenja = pas.Datum_Rodjenja;
                s.SpolVrsta = pas.SpolVrsta;
                s.VelicinaPsa = pas.VelicinaPsa;
                s.BojaPsa = pas.BojaPsa;
                s.MojaPrica = pas.MojaPrica;
                s.StatusOpis = pas.StatusOpis;
                s.Kastracija = pas.Kastracija;
                _context.Psi.Update(s);
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
                var s = _context.Psi.Find(sifra);
                if (s == null)
                {
                    return NotFound();
                }
                _context.Psi.Remove(s);
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
