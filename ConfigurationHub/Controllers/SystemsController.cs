using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Configuration.Data;
using ConfigurationHub.Domain;

namespace ConfigurationHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemsController : ControllerBase
    {
        private readonly ConfigurationContext _context;

        public SystemsController(ConfigurationContext context)
        {
            _context = context;
        }

        // GET: api/Systems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.System>>> GetSystems()
        {
            return await _context.Systems.ToListAsync();
        }

        // GET: api/Systems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.System>> GetSystem(int id)
        {
            var system = await _context.Systems
                .Include(t => t.Microservices)
                .FirstAsync(x => x.Id.Equals(id));

            if (system == null)
            {
                return NotFound();
            }

            return system;
        }

        // DELETE: api/Systems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSystem(int id)
        {
            var system = await _context.Systems.FindAsync(id);

            if (system == null)
            {
                return NotFound();
            }

            _context.Systems.Remove(system);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
