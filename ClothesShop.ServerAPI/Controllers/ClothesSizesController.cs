using ClothesShop.Domain.Models;
using ClothesShop.Persistence.Context;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothesShop.ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesSizesController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public ClothesSizesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET api/<ClothesSizesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            ClothesSize clothesSize = _context.ClothesSizes.FirstOrDefault(c => c.Id.Equals(id));
            if (clothesSize == null)
                return NotFound();
            return new ObjectResult(clothesSize);
        }

        // PUT api/<ClothesSizesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ClothesSize>> Put(ClothesSize clothesSize)
        {
            if (clothesSize == null)
            {
                return BadRequest();
            }
            if (!_context.ClothesSizes.Any(x => x.Id == clothesSize.Id))
            {
                return NotFound();
            }

            _context.Update(clothesSize);
            await _context.SaveChangesAsync();
            return Ok(clothesSize);
        }
    }
}
