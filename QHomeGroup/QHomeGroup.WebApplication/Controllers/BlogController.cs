using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Application.Content.Blogs;

namespace QHomeGroup.WebApplication.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [Route("tin-tuc.html")]
        public async Task<IActionResult> Index(string keyword, int page = 1)
        {
            var result = await _blogService.GetAllPagingWebApp(keyword, page, 4);
            return View(result);
        }
        [Route("tin-tuc.{alias}.{id}.html")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _blogService.GetDetails(id);
            return View(result);
        }
    }
}