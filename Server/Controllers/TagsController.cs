using BlazorMovies.Server.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlazorMovies.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly DataContext _context;

        public TagsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleTag(string id)
        {
            var tag = await _context.Tags
                .Include(t => t.Movies)
                .FirstOrDefaultAsync(t => t.TagId == id);
            if (tag == null)
                return NotFound("Tag wasn't found.");
            return Ok(tag);
        }
        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetTagsByName(string name)
        {
            var tags = await _context.Tags
                .Where(m => m.Name.ToLower().Contains(name.ToLower())).AsQueryable().ToListAsync();
            if (tags == null)
                return NotFound("Tags weren't found.");
            return Ok(tags);
        }
    }
}
