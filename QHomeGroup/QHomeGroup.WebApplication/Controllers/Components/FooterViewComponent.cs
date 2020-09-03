using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Application.Introduce;
using System.Threading.Tasks;

namespace QHomeGroup.WebApplication.Controllers.Components
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ICompanyIntroService _companyIntroService;

        public FooterViewComponent(ICompanyIntroService companyIntroService)
        {
            _companyIntroService = companyIntroService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var companyIntro = await _companyIntroService.Get();
            return View(companyIntro);
        }
    }
}