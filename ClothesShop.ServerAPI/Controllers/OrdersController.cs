using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothesShop.ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public OrdersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET api/<OrdersController>/user/5
        [HttpGet("user/{id}")]
        public IActionResult GetByUserId(Guid id)
        {
            Order order = _context.Orders.FirstOrDefault(c => c.UserId.Equals(id));
            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Order order = _context.Orders.FirstOrDefault(c => c.Id.Equals(id));
            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Put(Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            if (!_context.Orders.Any(x => x.Id == order.Id))
            {
                return NotFound();
            }

            _context.Update(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }
    }
}
