using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothesShop.ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public ManufacturersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<ManufacturersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> Get()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        // GET api/<ManufacturersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Manufacturer manufacturer = _context.Manufacturers.FirstOrDefault(c => c.Id.Equals(id));
            if (manufacturer == null)
                return NotFound();
            return new ObjectResult(manufacturer);
        }

    }
}
