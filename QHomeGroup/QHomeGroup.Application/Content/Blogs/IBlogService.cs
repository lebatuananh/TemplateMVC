using QHomeGroup.Application.Content.Blogs.Dtos;
using QHomeGroup.Utilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using QHomeGroup.Application.Content.Blogs.Request;

namespace QHomeGroup.Application.Content.Blogs
{
    public interface IBlogService
    {
        Task Add(UpdateBlogRequest updateBlogRequest);

        Task Update(int id, UpdateBlogRequest updateBlogRequest);

        Task<bool> Delete(int id);

        Task<BlogDto> Get(int id);

        Task<IList<BlogDto>> GetAll();

        Task<QueryResult<BlogDto>> GetAllPaging(string keyword, int skip, int take);

        Task<PagedResult<BlogDto>> GetAllPagingWebApp(string keyword, int page, int pageSize);

        Task<IList<BlogDto>> GetLastest(int top);

        Task<IList<BlogDto>> GetHotProduct(int top);
        Task SaveChangesAsync();
        Task<BlogDto> GetDetails(int id);
    }
}