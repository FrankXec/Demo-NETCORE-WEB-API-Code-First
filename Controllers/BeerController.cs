using DB;
using Demo_WebApi_CodeFirst.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_WebApi_CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly BarContext _context;

        public BeerController(BarContext context) {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Beer> GetBeers()
        { 
            return _context.Beers.ToArray();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            //var beer = _context.Beers.Find(id);
            var beer = _context.Beers.FirstOrDefault(b=>b.BeerId == id);
            if (beer != null) {
                return Ok(beer);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Beer>> Post(Beer beer)
        {
            //var beer = _context.Beers.Find(id);
            _context.Beers.Add(beer);
            await _context.SaveChangesAsync();

            return Ok(beer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Beer>> Put(int id,[FromBody]Beer beer)
        {
            //var beer = _context.Beers.Find(id);
            beer.BeerId = id;
            _context.Beers.Entry(beer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(beer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Beer>> Delete(int id)
        {
            var beer = _context.Beers.Find(id);
            if (beer is null) {
                return NotFound();
            }
            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            return Ok(beer);
        }
    }
}
