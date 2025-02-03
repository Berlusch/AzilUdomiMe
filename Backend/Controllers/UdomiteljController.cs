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
       


    }
}
