using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothesShop.ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public CartsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<CartsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> Get()
        {
            return await _context.Carts.ToListAsync();
        }

        // GET api/<CartsController>/user/5
        [HttpGet("user/{id}")]
        public IActionResult GetByUserId(Guid id)
        {
            Cart cart = _context.Carts.FirstOrDefault(c => c.UserId.Equals(id));
            if (cart == null)
                return NotFound();
            return new ObjectResult(cart);
        }

        // GET api/<CartsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Cart cart = _context.Carts.FirstOrDefault(c => c.Id.Equals(id));
            if (cart == null)
                return NotFound();
            return new ObjectResult(cart);
        }

        // POST api/<CartsController>
        [HttpPost]
        public async Task<ActionResult<Cart>> Post(Cart cart)
        {
            if (cart == null)
            {
                return BadRequest();
            }

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return Ok(cart);
        }

    }
}
