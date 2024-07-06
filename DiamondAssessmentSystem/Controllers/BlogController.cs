using Microsoft.AspNetCore.Mvc;
using DiamondAssessment.Interfaces;
using DiamondAssessmentSystem.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiamondAssessmentSystem.BusinessObject.DTO;
using DiamondAssessmentSystem.Models;

namespace DiamondAssessmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        // GET: api/Blog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetBlogs()
        {
            var blogs = await _blogRepository.GetBlogsAsync();
            var blogDtos = blogs.Select(blog => new BlogDto
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                BlogDate = blog.BlogDate,
                Description = blog.Description,
                Context = blog.Context,
                Image = blog.Image
            }).ToList();
            return Ok(blogDtos);
        }

        // GET: api/Blog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDto>> GetBlog(int id)
        {
            var blog = await _blogRepository.GetBlogByIdAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            var blogDto = new BlogDto
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                BlogDate = blog.BlogDate,
                Description = blog.Description,
                Context = blog.Context,
                Image = blog.Image
            };

            return Ok(blogDto);
        }

        // POST: api/Blog
        [HttpPost]
        public async Task<ActionResult<BlogDto>> PostBlog(BlogDto blogDto)
        {
            var blog = new Blog
            {
                BlogId = blogDto.BlogId,
                Title = blogDto.Title,
                BlogDate = blogDto.BlogDate,
                Description = blogDto.Description,
                Context = blogDto.Context,
                Image = blogDto.Image
            };

            var createdBlog = await _blogRepository.CreateBlogAsync(blog);
            var createdBlogDto = new BlogDto
            {
                BlogId = createdBlog.BlogId,
                Title = createdBlog.Title,
                BlogDate = createdBlog.BlogDate,
                Description = createdBlog.Description,
                Context = createdBlog.Context,
                Image = createdBlog.Image
            };

            return CreatedAtAction(nameof(GetBlog), new { id = createdBlogDto.BlogId }, createdBlogDto);
        }

        // PUT: api/Blog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, BlogDto blogDto)
        {
            if (id != blogDto.BlogId)
            {
                return BadRequest();
            }

            var blog = new Blog
            {
                BlogId = blogDto.BlogId,
                Title = blogDto.Title,
                BlogDate = blogDto.BlogDate,
                Description = blogDto.Description,
                Context = blogDto.Context,
                Image = blogDto.Image
            };

            var updated = await _blogRepository.UpdateBlogAsync(blog);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Blog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var deleted = await _blogRepository.DeleteBlogAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
