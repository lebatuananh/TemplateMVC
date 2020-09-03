using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Application.Projects;
using System.Threading.Tasks;
using static QHomeGroup.Utilities.Constants.CommonConstants;

namespace QHomeGroup.WebApplication.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IProjectService _projectService;
        public ServiceController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Route("dich-vu-{type}.html")]
        public async Task<IActionResult> Index(string type)
        {
            var service = await _projectService.GetServiceByType(type);
            return View(service);
        }
    }
}