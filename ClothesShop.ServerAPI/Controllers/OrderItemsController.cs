using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothesShop.ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public OrderItemsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<OrderItemsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> Get()
        {
            return await _context.OrderItems.ToListAsync();
        }

        // GET api/<OrderItemsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            OrderItem orderItem = _context.OrderItems.FirstOrDefault(c => c.Id.Equals(id));
            if (orderItem == null)
                return NotFound();
            return new ObjectResult(orderItem);
        }

        // POST api/<OrderItemsController>
        [HttpPost]
        public async Task<ActionResult<OrderItem>> Post(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                return BadRequest();
            }

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
            return Ok(orderItem);
        }

        // PUT api/<OrderItemsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderItem>> Put(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                return BadRequest();
            }
            if (!_context.OrderItems.Any(x => x.Id == orderItem.Id))
            {
                return NotFound();
            }

            _context.Update(orderItem);
            await _context.SaveChangesAsync();
            return Ok(orderItem);
        }

        // DELETE api/<OrderItemsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderItem>> Delete(Guid id)
        {
            OrderItem orderItem = _context.OrderItems.FirstOrDefault(x => x.Id.Equals(id));
            if (orderItem == null)
            {
                return NotFound();
            }
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            return Ok(orderItem);
        }
    }
}
