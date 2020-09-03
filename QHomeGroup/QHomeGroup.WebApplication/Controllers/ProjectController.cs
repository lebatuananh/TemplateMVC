using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Application.Common.Slides;
using QHomeGroup.Application.Projects;

namespace QHomeGroup.WebApplication.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ISlideService _slideService;

        public ProjectController(IProjectService projectService, ISlideService slideService)
        {
            _projectService = projectService;
            _slideService = slideService;
        }

        [Route("du-an.{type}.html")]
        public async Task<IActionResult> Index(int type, int page = 1)
        {
            var data = await _projectService.GetAllPagingWebApp(type, null, page, 12);
            ViewData["Banner"] = await _slideService.GetAll(Data.Entities.Content.SlideType.Project);
            return View(data);
        }
        [Route("chi-tiet-du-an.{alias}.{id}.html")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _projectService.GetDetails(id);
            return View(result);
        }
    }
}