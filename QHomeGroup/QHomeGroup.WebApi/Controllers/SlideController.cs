using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Application.Common.Slides;
using QHomeGroup.Application.Common.Slides.Request;
using QHomeGroup.Application.Content.Blogs.Request;

namespace QHomeGroup.WebApi.Controllers
{
    [Authorize]
    public class SlideController : V1Controller
    {
        private readonly ISlideService _slideService;

        public SlideController(ISlideService slideService)
        {
            _slideService = slideService;
        }


        [HttpGet]
        public async Task<IActionResult> Query(string query, int skip = 0, int take = 10)
        {
            var slides = await _slideService.GetAllPaging(query, skip, take);
            return Ok(slides);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var slide = await _slideService.Get(id);
            return Ok(slide);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSlideRequest request)
        {
            await _slideService.Update(id, request);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UpdateSlideRequest request)
        {
            await _slideService.Add(request);
            return Ok();
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _slideService.Delete(id);
            await _slideService.SaveChangesAsync();
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}