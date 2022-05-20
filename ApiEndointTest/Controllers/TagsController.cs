using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEndointTest.Data;
using ApiEndointTest.Models;
using ApiEndointTest.Requests;

namespace ApiEndointTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TagsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
          if (_context.Tags == null)
          {
              return NotFound();
          }
            return await _context.Tags.Include(t=>t.Posts).ToListAsync();
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
          if (_context.Tags == null)
          {
              return NotFound();
          }
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        // PUT: api/Tags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, TagPostRequest tag)
        {
            if (id != tag.Id)
            {
                return BadRequest();
            }
            Tag toUpdateTag = await _context.Tags.FindAsync(id);
            toUpdateTag.Name = tag.Name;
            toUpdateTag.Description = tag.Description;
            _context.Entry(toUpdateTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // POST: api/Tags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(TagPostRequest tag)
        {
          if (_context.Tags == null)
          {
              return Problem("Entity set 'AppDbContext.Tags'  is null.");
          }
            Tag newTag = new()
            {
                Id = tag.Id,
                Name = tag.Name,
                Description = tag.Description,
                Posts = new()
            };
            _context.Tags.Add(newTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTag", new { id = newTag.Id }, newTag);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            if (_context.Tags == null)
            {
                return NotFound();
            }
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(int id)
        {
            return (_context.Tags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
