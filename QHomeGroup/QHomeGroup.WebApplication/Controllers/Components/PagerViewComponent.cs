using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Utilities.Dtos;

namespace QHomeGroup.WebApplication.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}