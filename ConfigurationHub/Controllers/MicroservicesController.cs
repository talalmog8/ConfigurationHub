using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Configuration.Data;
using ConfigurationHub.Domain;
using ConfigurationHub.Domain.ConfigModels.MicroserviceModels;

namespace ConfigurationHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MicroservicesController : ControllerBase
    {
        private readonly ConfigurationContext _context;

        public MicroservicesController(ConfigurationContext context)
        {
            _context = context;
        }

        // GET: api/Microservices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Microservice>>> GetMicroServices()
        {
            return await _context.MicroServices.ToListAsync();
        }

        // GET: api/Microservices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Microservice>> GetMicroservice(int id)
        {
            var microservice = await _context.MicroServices.FindAsync(id);

            if (microservice == null)
            {
                return NotFound();
            }

            return microservice;
        }

        // PUT: api/Microservices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMicroservice(int id, Microservice microservice)
        {
            if (id != microservice.Id)
            {
                return BadRequest();
            }

            _context.Entry(microservice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MicroserviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Microservices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Microservice>> PostMicroservice(Microservice microservice)
        {
            _context.MicroServices.Add(microservice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMicroservice", new { id = microservice.Id }, microservice);
        }

        // DELETE: api/Microservices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMicroservice(int id)
        {
            var microservice = await _context.MicroServices.FindAsync(id);
            if (microservice == null)
            {
                return NotFound();
            }

            if (_context.Configs.Any(c => c.Microservice.Id.Equals(id)))
            {
                return BadRequest("There is a child configuration of this microservice");
            }

            _context.MicroServices.Remove(microservice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MicroserviceExists(int id)
        {
            return _context.MicroServices.Any(e => e.Id == id);
        }
    }
}
