using BlazorMovies.Server.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlazorMovies.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly DataContext _context;

        public DirectorsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleDirector(string id)
        {
            var director = await _context.Directors
                .Include(d => d.Movies)
                .FirstOrDefaultAsync(d => d.PersonId == id);
            if (director == null)
                return NotFound("Director wasn't found.");
            return Ok(director);
        }
        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetDirectorsByName(string name)
        {
            var directors = await _context.Directors
                .Where(m => m.Name.ToLower().Contains(name.ToLower())).AsQueryable().ToListAsync();
            if (directors == null)
                return NotFound("Directors weren't found.");
            return Ok(directors);
        }
    }
}
