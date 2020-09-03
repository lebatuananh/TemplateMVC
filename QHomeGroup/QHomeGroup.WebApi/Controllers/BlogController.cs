using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Application.Content.Blogs;
using QHomeGroup.Application.Content.Blogs.Request;

namespace QHomeGroup.WebApi.Controllers
{
    [Authorize]
    public class BlogController : V1Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public async Task<IActionResult> Query(string query, int skip = 0, int take = 10)
        {
            var blogs = await _blogService.GetAllPaging(query, skip, take);
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var blog = await _blogService.Get(id);
            return Ok(blog);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBlogRequest request)
        {
            await _blogService.Update(id, request);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UpdateBlogRequest request)
        {
            await _blogService.Add(request); 
            return Ok();
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _blogService.Delete(id);
            await _blogService.SaveChangesAsync();
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}