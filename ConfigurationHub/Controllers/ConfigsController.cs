using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Configuration.Data;
using ConfigurationHub.Domain;
using ConfigurationHub.Domain.Auth;
using ConfigurationHub.Domain.ConfigModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationHub.Controllers
{
    /// <summary>
    /// Auto Generated Controller For Config Model.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigsController : ControllerBase
    {
        private readonly ConfigurationContext _context;
        private readonly IMapper _mapper;

        public ConfigsController(ConfigurationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Configs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SavedConfigDto>>> GetConfigs(int skip, int take)
        {
            return await _context.Configs
                .Include(x => x.Author)
                .Include(y => y.ConfigContent)
                .Include(t => t.Microservice)
                .ThenInclude(t => t.System)
                .OrderByDescending(o => o.LastModified)
                .Skip(skip)
                .Take(take)
                .Select(u => _mapper.Map<SavedConfigDto>(u))
                .ToListAsync();
        }

        // GET: api/Configs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SavedConfigDto>> GetConfig(int id)
        {
            var config = await _context.Configs
                .Include(x => x.Author)
                .Include(y => y.ConfigContent)
                .Include(t => t.Microservice)
                .ThenInclude(t => t.System)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (config == null)
            {
                return NotFound();
            }

            return _mapper.Map<SavedConfigDto>(config);
        }

        // PUT: api/Configs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")] [Authorize(policy: "user")]
        public async Task<IActionResult> PutConfig(int id, Config config)
        {
            if (id != config.Id)
            {
                return BadRequest();
            }

            _context.Attach(config);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Configs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SavedConfigDto>> PostConfig(NewConfigDto configDto)
        {
            var config = _mapper.Map<Config>(configDto);
            
            config.Author = new User
            {
                Id = int.Parse(HttpContext.User.Identity.Name)
            };
            
            _context.Configs.Attach(config);
            await _context.SaveChangesAsync();
            
            SavedConfigDto result = _mapper.Map<SavedConfigDto>(await _context.Configs
                .Include(o => o.ConfigContent)
                .Include(u => u.Microservice)
                .ThenInclude(u => u.System)
                .FirstAsync(c => c.Id.Equals(config.Id)));

            return CreatedAtAction("GetConfig", new { id = result.Id }, result);
        }

        // DELETE: api/Configs/5
        [HttpDelete("{id}")] [Authorize(policy: "user")]
        public async Task<IActionResult> DeleteConfig(int id)
        {
            var config = await _context.Configs.FindAsync(id);

            if (config == null)
            {
                return NotFound();
            }

            _context.Configs.Remove(config);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConfigExists(int id)
        {
            return _context.Configs.Any(e => e.Id == id);
        }
    }
}
