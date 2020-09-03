using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Application.Introduce;
using QHomeGroup.Application.Projects;
using System;
using System.Threading.Tasks;

namespace QHomeGroup.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeConfigService _homeConfigService;

        public HomeController(IHomeConfigService homeConfigService)
        {
            _homeConfigService = homeConfigService;
        }

        public async Task<IActionResult> Index()
        {
            var homeConfig = await _homeConfigService.Get();
            return View(homeConfig);
        }

        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            string[] availableLanguages = { "en", "vi" };
            if (Array.Exists(availableLanguages, element => element == culture))
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            return LocalRedirect(returnUrl);
        }
    }
}