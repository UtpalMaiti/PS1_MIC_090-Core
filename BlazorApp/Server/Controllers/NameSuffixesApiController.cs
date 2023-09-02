using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Server.Data;
using BlazorApp.Server.Models;

namespace BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameSuffixesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NameSuffixesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/NameSuffixesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NameSuffix>>> GetNameSuffix()
        {
            if (_context.NameSuffix == null)
            {
                return NotFound();
            }
            return await _context.NameSuffix.ToListAsync();
        }

        // GET: api/NameSuffixesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NameSuffix>> GetNameSuffix(int id)
        {
            if (_context.NameSuffix == null)
            {
                return NotFound();
            }
            var nameSuffix = await _context.NameSuffix.FindAsync(id);

            if (nameSuffix == null)
            {
                return NotFound();
            }

            return nameSuffix;
        }

        // PUT: api/NameSuffixesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNameSuffix(int id, NameSuffix nameSuffix)
        {
            if (id != nameSuffix.SuffixId)
            {
                return BadRequest();
            }

            _context.Entry(nameSuffix).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NameSuffixExists(id))
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

        // POST: api/NameSuffixesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NameSuffix>> PostNameSuffix(NameSuffix nameSuffix)
        {
            if (_context.NameSuffix == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NameSuffix'  is null.");
            }
            _context.NameSuffix.Add(nameSuffix);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNameSuffix", new { id = nameSuffix.SuffixId }, nameSuffix);
        }

        // DELETE: api/NameSuffixesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNameSuffix(int id)
        {
            if (_context.NameSuffix == null)
            {
                return NotFound();
            }
            var nameSuffix = await _context.NameSuffix.FindAsync(id);
            if (nameSuffix == null)
            {
                return NotFound();
            }

            _context.NameSuffix.Remove(nameSuffix);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NameSuffixExists(int id)
        {
            return (_context.NameSuffix?.Any(e => e.SuffixId == id)).GetValueOrDefault();
        }
    }
}
