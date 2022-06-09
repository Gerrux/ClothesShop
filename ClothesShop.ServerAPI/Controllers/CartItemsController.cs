using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothesShop.ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public CartItemsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<CartItemsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> Get()
        {
            return await _context.CartItems.ToListAsync();
        }

        // GET api/<CartItemsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            CartItem cartItem = _context.CartItems.FirstOrDefault(c => c.Id.Equals(id));
            if (cartItem == null)
                return NotFound();
            return new ObjectResult(cartItem);
        }

        // POST api/<CartItemsController>
        [HttpPost]
        public async Task<ActionResult<CartItem>> Post(CartItem cartItem)
        {
            if (cartItem == null)
            {
                return BadRequest();
            }

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return Ok(cartItem);
        }

        // PUT api/<CartItemsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CartItem>> Put(CartItem cartItem)
        {
            if (cartItem == null)
            {
                return BadRequest();
            }
            if (!_context.CartItems.Any(x => x.Id == cartItem.Id))
            {
                return NotFound();
            }

            _context.Update(cartItem);
            await _context.SaveChangesAsync();
            return Ok(cartItem);
        }

        // DELETE api/<CartItemsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartItem>> Delete(Guid id)
        {
            CartItem cartItem = _context.CartItems.FirstOrDefault(x => x.Id.Equals(id));
            if (cartItem == null)
            {
                return NotFound();
            }
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return Ok(cartItem);
        }
    }
}
