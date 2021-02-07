using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Configuration.Data;
using ConfigurationHub.Domain.ConfigModels.MicroserviceModels;

namespace ConfigurationHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MicroservicesController : ControllerBase
    {
        private readonly ConfigurationContext _context;
        private readonly IMapper _mapper;

        public MicroservicesController(ConfigurationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Microservices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SavedMicroServiceDto>>> GetMicroServices(int skip, int take)
        {
            return await _context.MicroServices
                .Skip(skip)
                .Take(take)
                .Include(k => k.System)
                .Select(y => _mapper.Map<SavedMicroServiceDto>(y))
                .ToListAsync();
        }

        // GET: api/Microservices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SavedMicroServiceWithConfigsDto>> GetMicroservice(int id)
        {
            var microservice = await _context.MicroServices
                .Include(c => c.System)
                .Include(c => c.Configs)
                .Where(x => x.Id.Equals(id))
                .Select(x => new SavedMicroServiceWithConfigsDto
                {
                    MicroService = _mapper.Map<SavedMicroServiceDto>(x),
                    ConfigIds = x.Configs.Select(t => t.Id)
                })
                .FirstAsync();

            if (microservice == null)
            {
                return NotFound();
            }

            return microservice;
        }

        // PUT: api/Microservices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<SavedMicroServiceDto>> PutMicroservice(int id, UpdatedMicroserviceDto microserviceDto)
        {
            if (!(MicroserviceExists(id) && (id == microserviceDto.Id)))
                return BadRequest("microservice with this id does not exist");

            _context.Entry(_mapper.Map<Microservice>(microserviceDto)).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return _mapper.Map<SavedMicroServiceDto>(
                await _context.MicroServices
                    .Include(y => y.System)
                    .FirstAsync(c => c.Id.Equals(id)));
        }

        // POST: api/Microservices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost] 
        public async Task<ActionResult<SavedMicroServiceDto>> PostMicroservice(NewMicroServiceDto microserviceDto)
        {
            var microservice = _mapper.Map<Microservice>(microserviceDto);

            _context.MicroServices.Attach(microservice);
            await _context.SaveChangesAsync();

            var savedMicroservice = _mapper.Map<SavedMicroServiceDto>(await _context.MicroServices.Include(c => c.System)
                .FirstAsync(c => c.Id.Equals(microservice.Id)));

            return CreatedAtAction("GetMicroservice", new { id = savedMicroservice.Id }, savedMicroservice);
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
