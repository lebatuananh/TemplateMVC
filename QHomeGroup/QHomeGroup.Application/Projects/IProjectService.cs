using QHomeGroup.Application.Projects.Dtos;
using QHomeGroup.Application.Projects.Requests;
using QHomeGroup.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QHomeGroup.Application.Projects
{
    public interface IProjectService : IDisposable
    {
        Task<QueryResult<ProjectDto>> GetAllPaging(int skip, int take, string query);
        Task<ProjectDto> Get(int id);
        Task<ProjectDto> GetDetails(int id);
        Task Create(UpdateProjectRequest request);
        Task Update(int id, UpdateProjectRequest request);
        Task<bool> Delete(int id);
        Task<QueryResult<ServiceDto>> GetServicePaging(int skip, int take, string query);

        Task<IList<ServiceDto>> GetAllServices();

        Task CreateService(UpdateServiceRequest request);

        Task UpdateService(int id, UpdateServiceRequest request);

        Task<ServiceDto> GetService(int id);

        Task<ServiceDto> GetServiceByType(string type);

        Task<bool> DeleteService(int id);

        Task SaveChangesAsync();
        Task<PagedResult<ProjectDto>> GetAllPagingWebApp(int type,string keyword, int page, int pageSize);
        Task<ProjectDto> GetOutStandingProject();
    }
}