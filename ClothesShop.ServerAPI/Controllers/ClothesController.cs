using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothesShop.ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public ClothesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<ClothesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clothes>>> Get()
        {
            return await _context.Clothes.Include(clt => clt.Type).Include(clt => clt.Manufacturer).Include(clt => clt.Images).Include(clt => clt.Sizes).ToListAsync();
        }

        // GET: api/<ClothesController>/women
        [HttpGet("women")]
        public async Task<ActionResult<IEnumerable<Clothes>>> GetWomen()
        {
            return await _context.Clothes.Include(clt => clt.Type)
                .Include(clt => clt.Manufacturer).Include(clt => clt.Images)
                .Include(clt => clt.Sizes)
                .Where(clt => clt.Gender.Equals(GenderNames.Woman) || clt.Gender.Equals(GenderNames.Unisex))
                .ToListAsync();
        }

        // GET: api/<ClothesController>/men
        [HttpGet("men")]
        public async Task<ActionResult<IEnumerable<Clothes>>> GetMen()
        {
            return await _context.Clothes.Include(clt => clt.Type)
                .Include(clt => clt.Manufacturer).Include(clt => clt.Images)
                .Include(clt => clt.Sizes)
                .Where(clt => clt.Gender.Equals(GenderNames.Man) || clt.Gender.Equals(GenderNames.Unisex))
                .ToListAsync();
        }

        // GET api/<ClothesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Clothes clothes = _context.Clothes.Include(clt => clt.Type).Include(clt => clt.Manufacturer).Include(clt => clt.Images).Include(clt => clt.Sizes).FirstOrDefault(c => c.Id.Equals(id));
            if (clothes == null)
                return NotFound();
            return new ObjectResult(clothes);
        }
    }
}
