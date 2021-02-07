using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Configuration.Data;
using ConfigurationHub.Domain;
using ConfigurationHub.Domain.Auth;
using ConfigurationHub.Domain.ConfigModels;
using ConfigurationHub.Domain.ConfigModels.Content;
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

        [HttpGet("user/{username}")]
        public async Task<ActionResult<IEnumerable<SavedConfigDto>>> GetConfigByUser(string username, int skip, int take)
        {
            return await _context.Users
                .Where(c => c.Username.Equals(username))
                .SelectMany(c => c.Configs)
                .Skip(skip)
                .Take(take)
                .Include(c => c.ConfigContent)
                .Include(c => c.Microservice)
                .ThenInclude(c => c.System)
                .Select(c => _mapper.Map<SavedConfigDto>(c))
                .ToListAsync();
        }

        // PUT: api/Configs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfig(int id, UpdateConfigDto configDto)
        {
            if (!(ConfigExists(id) && configDto.Id.Equals(id)))
            {
                return BadRequest("this configuration doesn't exist in this manner");
            }

            var config = await _context.Configs
                .Where(x => x.Id.Equals(id))
                .Include(x => x.ConfigContent)
                .Include(x => x.Author)
                .FirstAsync();

            if (config.Author.Id.ToString() != ControllerContext.HttpContext.User.Identity.Name)
                return BadRequest("you are not the owner of this configuration");

            if (!config.ConfigContent.Id.Equals(configDto.ConfigContent.Id))
                return BadRequest("mismatching config content ids");

            _context.Configs.Attach(config);
            config.ConfigContent = _mapper.Map<ConfigContent>(configDto.ConfigContent);

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

            return Ok(new { ConfigId = config.Id, ConfigContentId = config.ConfigContent.Id, UpdatedContent = config.ConfigContent.Content });
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
        [HttpDelete("{id}")]
        [Authorize(policy: "user")]
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
