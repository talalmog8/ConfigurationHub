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
    public class ConfigAuthorsController : ControllerBase
    {
        private readonly ConfigurationContext _context;

        public ConfigAuthorsController(ConfigurationContext context)
        {
            _context = context;
        }

        // GET: api/ConfigAuthors
        [HttpGet("{skip}/{limit}")]
        public async Task<ActionResult<IEnumerable<AuthorAndConfigs>>> GetConfigAuthors(int limit, int skip = 0)
        {
            return  await _context.ConfigAuthors
                .Skip(skip)
                .Take(limit)
                .Select(x => new AuthorAndConfigs()
                {
                    ConfigAuthor = x, 
                    ConfigIds  = x.Configs.Select(t => t.Id),
                    ConfigCount = x.Configs.Count
                }).ToListAsync();
        }

        // GET: api/ConfigAuthors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConfigAuthor>> GetConfigAuthor(int id)
        {
            var configAuthor = await _context.ConfigAuthors.FindAsync(id);

            if (configAuthor == null)
            {
                return NotFound();
            }

            return configAuthor;
        }

        // PUT: api/ConfigAuthors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfigAuthor(int id, ConfigAuthor configAuthor)
        {
            if (id != configAuthor.Id)
            {
                return BadRequest();
            }

            _context.Entry(configAuthor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigAuthorExists(id))
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

        // POST: api/ConfigAuthors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConfigAuthor>> PostConfigAuthor(ConfigAuthor configAuthor)
        {
            _context.ConfigAuthors.Add(configAuthor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfigAuthor", new { id = configAuthor.Id }, configAuthor);
        }

        // DELETE: api/ConfigAuthors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfigAuthor(int id)
        {
            var configAuthor = await _context.ConfigAuthors.FindAsync(id);
            if (configAuthor == null)
            {
                return NotFound();
            }

            _context.ConfigAuthors.Remove(configAuthor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConfigAuthorExists(int id)
        {
            return _context.ConfigAuthors.Any(e => e.Id == id);
        }
    }
}
