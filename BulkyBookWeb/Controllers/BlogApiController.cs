using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Models;
using BulkyBookWeb.Data;

namespace BulkyBookWeb.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BlogApiController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            Blog? obj = _db.Blogs.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            // Return the blog as JSON
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult CreateBlog([FromBody] Blog obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (obj.Title == obj.Category)
            {
                ModelState.AddModelError("Title", "The title and category cannot be the same");
                return BadRequest(ModelState);
            }

            _db.Blogs.Add(obj);
            _db.SaveChanges();

            // Return the created blog as JSON
            return CreatedAtAction(nameof(GetBlog), new { id = obj.Id }, obj);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, [FromBody] Blog obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj.Id)
            {
                return BadRequest();
            }

            if (obj.Title == obj.Category)
            {
                ModelState.AddModelError("Title", "The title and category cannot be the same");
                return BadRequest(ModelState);
            }

            _db.Blogs.Update(obj);
            _db.SaveChanges();

            // Return the updated blog as JSON
            return Ok(obj);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            Blog? obj = _db.Blogs.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Blogs.Remove(obj);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
