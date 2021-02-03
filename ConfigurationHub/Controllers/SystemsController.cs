using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Configuration.Data;
using ConfigurationHub.Domain;
using ConfigurationHub.Domain.ConfigModels.SystemModels;

namespace ConfigurationHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemsController : ControllerBase
    {
        private readonly ConfigurationContext _context;
        private readonly IMapper _mapper;

        public SystemsController(ConfigurationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Systems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemWithMicroservicesDto>>> GetSystems(int skip, int take)
        {
            return await _context.Systems
                .Skip(skip)
                .Take(take)
                .Include(s => s.Microservices)
                .Select(c => _mapper.Map<SystemWithMicroservicesDto>(c))
                .ToListAsync();
        }

        // GET: api/Systems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemWithMicroservicesDto>> GetSystem(int id)
        {
            var system = await _context.Systems
                .Include(t => t.Microservices)
                .Where(x => x.Id.Equals(id))
                .Select(x => _mapper.Map<SystemWithMicroservicesDto>(x))
                .FirstAsync();

            if (system == null)
            {
                return NotFound();
            }

            return system;
        }

        [HttpPost]
        public async Task<ActionResult<SavedSystemDto>> PostMicroservice(NewSystemDto systemDto)
        {
            var system = _mapper.Map<Domain.ConfigModels.SystemModels.System>(systemDto);

            _context.Systems.Add(system);
            await _context.SaveChangesAsync();

            var savedSystem = _mapper.Map<SavedSystemDto>(await _context.Systems.FirstAsync(c => c.Id.Equals(system.Id)));

            return CreatedAtAction("GetSystem", new { id = savedSystem.Id }, savedSystem);
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

            if (_context.Configs.Any(c => c.Microservice.System.Id.Equals(id)))
            {
                return BadRequest("There is a child configuration of this system");
            }

            _context.Systems.Remove(system);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
