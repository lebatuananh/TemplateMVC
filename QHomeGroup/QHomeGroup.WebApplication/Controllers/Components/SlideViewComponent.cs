using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Application.Common.Slides;
using QHomeGroup.Data.Enum;
using System.Linq;
using System.Threading.Tasks;

namespace QHomeGroup.WebApplication.Controllers.Components
{
    public class SlideViewComponent : ViewComponent
    {
        private readonly ISlideService _slideService;

        public SlideViewComponent(ISlideService slideService)
        {
            _slideService = slideService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _slideService.GetAll(Data.Entities.Content.SlideType.Home);
            result = result.Where(x => x.Status == Status.Active).ToList();
            return View(result);
        }
    }
}