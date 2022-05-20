using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEndointTest.Data;
using ApiEndointTest.Models;
using ApiEndointTest.DTOs;

namespace ApiEndointTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            return await _context.Posts.Include(p => p.Author).Include(p => p.Tags).ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.Include(p => p.Author).Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, PostPostRequest post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }
            Post toUpdatePost = await _context.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id);
            toUpdatePost.Title = post.Title;
            toUpdatePost.Description = post.Description;
            toUpdatePost.PostedDate = post.PostedDate;
            toUpdatePost.AuthorId = post.UserId;
            List<Tag> tags = new();
            foreach (int tagId in post.TagIds)
            {
                tags.Add(_context.Tags.SingleOrDefault(t => t.Id == tagId));
            }
            toUpdatePost.Tags = tags;
            _context.Entry(toUpdatePost).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(PostPostRequest post)
        {
            if (_context.Posts is null || _context.Tags is null || _context.Users is null)
            {
                return Problem("Entity set 'AppDbContext.Posts'  is null.");
            }
            var user = _context.Users.SingleOrDefault(u => u.Id == post.UserId);
            List<Tag> tags = new();
            foreach (int tagId in post.TagIds)
            {
                tags.Add(_context.Tags.SingleOrDefault(t => t.Id == tagId));
            }
            Post newPost = new()
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                PostedDate = post.PostedDate,
                AuthorId = post.UserId

            };
            newPost.Tags = tags;
            newPost.Author = user;
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = newPost.Id }, newPost);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
