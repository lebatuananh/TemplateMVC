using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Application.Introduce;
using QHomeGroup.Application.Introduce.Request;

namespace QHomeGroup.WebApi.Controllers
{
    [Authorize]
    public class IntroduceController : V1Controller
    {
        private readonly IIntroduceConfigService _introduceConfigService;
        private readonly IHomeConfigService _homeConfigService;
        private readonly ICompanyIntroService _companyIntroService;

        public IntroduceController(IIntroduceConfigService introduceConfigService, IHomeConfigService homeConfigService, ICompanyIntroService companyIntroService)
        {
            _introduceConfigService = introduceConfigService;
            _homeConfigService = homeConfigService;
            _companyIntroService = companyIntroService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var config = await _introduceConfigService.GetByKey(key);
            return Ok(config);
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> Update(string key, IntroduceConfigRequest request)
        {
            await _introduceConfigService.Update(key, request);
            return Ok();
        }

        [HttpGet("home")]
        public async Task<IActionResult> GetHomeConfig()
        {
            var config = await _homeConfigService.Get();
            return Ok(config);
        }

        [HttpPost("home")]
        public async Task<IActionResult> UpdateHomeConfig(HomeConfigRequest request)
        {
            await _homeConfigService.UpdateHomeConfig(request);
            return Ok();
        }

        [HttpGet("company")]
        public async Task<IActionResult> GetCompanyIntro()
        {
            var config = await _companyIntroService.Get();
            return Ok(config);
        }

        [HttpPost("company")]
        public async Task<IActionResult> UpdateCompanyIntro(CompanyIntroRequest request)
        {
            await _companyIntroService.Update(request);
            return Ok();
        }
    }
}