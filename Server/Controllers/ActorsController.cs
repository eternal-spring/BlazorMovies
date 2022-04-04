using BlazorMovies.Server.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlazorMovies.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly DataContext _context;

        public ActorsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleActor(string id)
        {
            var actor = await _context.Actors
                .Include(a => a.Movies)
                .FirstOrDefaultAsync(a => a.PersonId == id);
            if (actor == null)
                return NotFound("Actor wasn't found.");

            return Ok(actor);
        }
        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetActorsByName(string name)
        {
            var actors = await _context.Actors
                .Where(m => m.Name.ToLower().Contains(name.ToLower())).AsQueryable().ToListAsync();
            if (actors == null)
                return NotFound("Actors weren't found.");
            return Ok(actors);
        }
    }
}
