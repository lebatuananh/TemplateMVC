using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QHomeGroup.Application.Projects.Dtos;
using QHomeGroup.Application.Projects.Requests;
using QHomeGroup.Data.Entities.Projects;
using QHomeGroup.Infrastructure.Interfaces;
using QHomeGroup.Utilities.Constants;
using QHomeGroup.Utilities.Dtos;
using QHomeGroup.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QHomeGroup.Application.Content.Blogs.Dtos;
using QHomeGroup.Data.Enum;
using System.Text.RegularExpressions;

namespace QHomeGroup.Application.Projects
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project, int> _projectRepository;
        private readonly IRepository<Service, int> _serviceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IRepository<Project, int> projectRepository, IRepository<Service, int> serviceRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _projectRepository = projectRepository;
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<QueryResult<ProjectDto>> GetAllPaging(int skip, int take, string query)
        {
            var queryResult = await _projectRepository.QueryAsync(t => string.IsNullOrEmpty(query) || EF.Functions.Like(t.Name, $"%{query}%"), skip, take);
            return queryResult.To<QueryResult<ProjectDto>>();
        }



        public async Task<ProjectDto> Get(int id)
        {
            var project = await _projectRepository.FindByIdAsync(id);
            return project.To<ProjectDto>();
        }

        public async Task<ProjectDto> GetDetails(int id)
        {
            var project = await _projectRepository.FindByIdAsync(id);
            var projects = await _projectRepository.GetManyAsync(x => x.OptionProject == project.OptionProject);
            if (project != null)
            {
                projects.Remove(project);
            }

            var result = project.To<ProjectDto>();
            var listData = new List<ProjectDto>();
            foreach (var item in projects)
            {
                listData.Add(new ProjectDto()
                {
                    Status = item.Status,
                    Description = item.Description,
                    Name = item.Name,
                    SlideOption = item.SlideOption,
                    SlideVideos = item.SlideVideos,
                    Block = item.Block,
                    OptionProject = item.OptionProject,
                    SlideImages = item.SlideImages,
                    Thumbnail = item.Thumbnail,
                    SeoAlias = item.SeoAlias,
                    CreatedBy = item.CreatedBy,
                    DateCreated = item.DateCreated,
                    DateModified = item.DateModified,
                    Id = item.Id,
                    LinkDetail = "chi-tiet-du-an." + item.SeoAlias + "." + item.Id + ".html"
                });
            }

            result.RelatedProjects = listData;
            return result;

        }

        public async Task Create(UpdateProjectRequest request)
        {
            Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.User?.FindFirst(SystemConstants.UserClaim.Id)?.Value);
            var project = new Project(request.Name, request.Description, request.Thumbnail, request.Block, request.OptionProject, request.Status, request.ServiceId, userId, request.SlideOption, request.SlideVideos, request.SlideImages);
            _projectRepository.Add(project);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(int id, UpdateProjectRequest request)
        {
            var project = await _projectRepository.FindByIdAsync(id);
            if (project != null)
            {
                project.Update(request.Name, request.Description, request.Thumbnail, request.Block, request.OptionProject, request.Status, request.ServiceId, request.SlideOption, request.SlideVideos, request.SlideImages);
                _projectRepository.Update(project);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<bool> Delete(int id)
        {
            var project = await _projectRepository.FindByIdAsync(id);
            if (project != null)
            {
                _projectRepository.Remove(project);
                return true;
            }
            return false;
        }

        public async Task<QueryResult<ServiceDto>> GetServicePaging(int skip, int take, string query)
        {
            var queryResult = await _serviceRepository.QueryAsync(t => string.IsNullOrEmpty(query) || EF.Functions.Like(t.Name, $"%{query}%"), skip, take);
            return queryResult.To<QueryResult<ServiceDto>>();
        }

        public async Task<bool> DeleteService(int id)
        {
            var service = await _serviceRepository.FindByIdAsync(id);
            if (service != null)
            {
                _serviceRepository.Remove(service);
                return true;
            }
            return false;
        }

        public async Task<ServiceDto> GetService(int id)
        {
            var service = await _serviceRepository.FindByIdAsync(id);
            return service.To<ServiceDto>();
        }

        public async Task UpdateService(int id, UpdateServiceRequest request)
        {
            var service = await _serviceRepository.FindByIdAsync(id);
            if (service != null)
            {
                service.UpdateService(request.Name, request.VideoUrl, request.Thumbnail, request.Block, request.SlideImages);
                _serviceRepository.Update(service);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task CreateService(UpdateServiceRequest request)
        {
            var service = new Service(request.Name, request.VideoUrl, request.Thumbnail, request.Block, request.SlideImages);
            _serviceRepository.Add(service);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResult<ProjectDto>> GetAllPagingWebApp(int type, string keyword, int page, int pageSize)
        {
            var listProject = await _projectRepository.GetManyAsync(x => x.Status == Status.Active);
            if (type == 0)
            {
                listProject = listProject.Where(x => x.OptionProject == OptionProject.Classic).ToList();
            }
            else if (type == 1)
            {
                listProject = listProject.Where(x => x.OptionProject == OptionProject.Modern).ToList();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                listProject = listProject.Where(x => x.Name.Contains(keyword)).ToList();
            }

            int totalRow = listProject.Count;
            listProject = listProject.OrderBy(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var data = new List<ProjectDto>();
            foreach (var item in listProject)
            {
                data.Add(new ProjectDto()
                {
                    Name = item.Name,
                    Description = item.Description,
                    DateCreated = item.DateCreated,
                    Status = item.Status,
                    Thumbnail = item.Thumbnail,
                    Block = item.Block,
                    CreatedBy = item.CreatedBy,
                    DateModified = item.DateModified,
                    Id = item.Id,
                    SlideImages = item.SlideImages,
                    SlideOption = item.SlideOption,
                    SlideVideos = item.SlideVideos,
                    SeoAlias = item.SeoAlias,
                    LinkDetail = "chi-tiet-du-an." + item.SeoAlias + "." + item.Id + ".html"
                });
            }
            var paginationSet = new PagedResult<ProjectDto>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public async Task<ProjectDto> GetOutStandingProject()
        {
            var result = await _projectRepository.GetAllAsync();
            var projectResult = result.Take(1).FirstOrDefault().To<ProjectDto>();
            return projectResult;
        }

        public async Task<IList<ServiceDto>> GetAllServices()
        {
            var services = await _serviceRepository.GetAllAsync();
            return services.To<IList<ServiceDto>>();
        }

        public async Task<ServiceDto> GetServiceByType(string type)
        {
            var service = await _serviceRepository.FindSingleAsync(s => s.SeoAlias == type);
            var serviceDto = service.To<ServiceDto>();
            serviceDto.EmbededId = Regex.Match(serviceDto.VideoUrl, @"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?)\/|.*[?&amp;]v=)|youtu\.be\/)([^""&amp;?\/ ]{11})").Groups[1].Value;
            var projects = await _projectRepository.QueryAsync(x => x.ServiceId == service.Id, 0, 10);
            var data = new List<ProjectDto>();
            foreach (var item in projects.Items)
            {
                data.Add(new ProjectDto()
                {
                    Name = item.Name,
                    Description = item.Description,
                    DateCreated = item.DateCreated,
                    Status = item.Status,
                    Thumbnail = item.Thumbnail,
                    Block = item.Block,
                    CreatedBy = item.CreatedBy,
                    DateModified = item.DateModified,
                    Id = item.Id,
                    SlideImages = item.SlideImages,
                    SlideOption = item.SlideOption,
                    SlideVideos = item.SlideVideos,
                    SeoAlias = item.SeoAlias,
                    LinkDetail = "chi-tiet-du-an." + item.SeoAlias + "." + item.Id + ".html"
                });
            }
            serviceDto.Projects = data;
            return serviceDto;
        }
    }
}