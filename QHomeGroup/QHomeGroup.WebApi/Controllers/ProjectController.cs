using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Application.Projects;
using QHomeGroup.Application.Projects.Requests;
using QHomeGroup.Data.Enum;

namespace QHomeGroup.WebApi.Controllers
{
    [Authorize]
    public class ProjectController : V1Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        #region Projects
        [HttpGet]
        public async Task<IActionResult> Query(string query, int skip = 0, int take = 10)
        {
            var projects = await _projectService.GetAllPaging(skip, take, query);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _projectService.Get(id);
            return Ok(project);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProjectRequest request)
        {
            await _projectService.Update(id, request);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UpdateProjectRequest request)
        {
            await _projectService.Create(request);
            return Ok();
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _projectService.Delete(id);
            await _projectService.SaveChangesAsync();
            if (result)
                return Ok();
            return BadRequest();
        }
        #endregion

        #region Services
        [HttpGet("services")]
        public async Task<IActionResult> QueryServices(string query, int skip = 0, int take = 10)
        {
            var services = await _projectService.GetServicePaging(skip, take, query);
            return Ok(services);
        }

        [HttpGet("services/getall")]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _projectService.GetAllServices();
            return Ok(services);
        }

        [HttpPost("services")]
        public async Task<IActionResult> CreateService([FromBody] UpdateServiceRequest request)
        {
            await _projectService.CreateService(request);
            return Ok();
        }

        [HttpPost("services/{id}")]
        public async Task<IActionResult> UpdateService([FromRoute] int id, [FromBody] UpdateServiceRequest request)
        {
            await _projectService.UpdateService(id, request);
            return Ok();
        }

        [HttpGet("services/{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            var service = await _projectService.GetService(id);
            return Ok(service);
        }

        [HttpDelete("service/{id}/delete")]
        public async Task<IActionResult> DeleteService([FromRoute] int id)
        {
            var result = await _projectService.DeleteService(id);
            await _projectService.SaveChangesAsync();
            if (result)
                return Ok();
            return BadRequest();
        }
        #endregion
    }
}