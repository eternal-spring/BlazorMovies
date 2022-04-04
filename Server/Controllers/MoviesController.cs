using Microsoft.AspNetCore.Mvc;
using BlazorMovies.Server.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlazorMovies.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _context;

        public MoviesController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetTop100Movies()
        {
            return Ok(await _context.Movies.OrderByDescending(m => m.Rating).Take(100).ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleMovie(string id)
        {
            var movie = await _context.Movies
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .Include(m => m.Tags)
                .FirstOrDefaultAsync(m => m.ImdbId == id);
            if (movie == null)
                return NotFound("Movie wasn't found.");
            return Ok(movie);
        }
        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetMoviesByName(string name)
        {
            var movies = await _context.Movies
                .Where(m => m.EngName.ToLower().Contains(name.ToLower()) || m.RuName.ToLower().Contains(name.ToLower())).AsQueryable().ToListAsync();
            if (movies == null)
                return NotFound("Movies weren't found.");
            return Ok(movies);
        }
    }
}
