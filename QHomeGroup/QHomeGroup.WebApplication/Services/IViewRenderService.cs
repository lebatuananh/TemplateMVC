using System.Threading.Tasks;

namespace QHomeGroup.WebApplication.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}