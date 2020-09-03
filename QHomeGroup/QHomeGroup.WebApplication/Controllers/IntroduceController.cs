using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Application.Introduce;
using System.Threading.Tasks;
using static QHomeGroup.Utilities.Constants.CommonConstants;

namespace QHomeGroup.WebApplication.Controllers
{
    public class IntroduceController : Controller
    {
        private readonly IIntroduceConfigService _introduceConfigService;

        public IntroduceController(IIntroduceConfigService introduceConfigService)
        {
            _introduceConfigService = introduceConfigService;
        }

        [Route("gioi-thieu.html")]
        public async Task<IActionResult> Index()
        {
            var configs = await _introduceConfigService.GetAll();
            return View(configs);
        }

        [Route("loi-ngo.html")]
        public async Task<IActionResult> Preface()
        {            
            return View();
        }

        [Route("tam-nhin-su-menh.html")]
        public async Task<IActionResult> Vision()
        {
            var config = await _introduceConfigService.GetByKey(IntroduceKey.Vision);
            return View(config);
        }

        [Route("gia-tri-cot-loi.html")]
        public async Task<IActionResult> CoreValue()
        {
            var config = await _introduceConfigService.GetByKey(IntroduceKey.CoreValue);
            return View(config);
        }

        [Route("lich-su-hinh-thanh.html")]
        public async Task<IActionResult> HistoryBegin()
        {
            var config = await _introduceConfigService.GetByKey(IntroduceKey.HistoryBegin);
            return View(config);
        }

        [Route("chat-luong-dich-vu.html")]
        public async Task<IActionResult> ServiceQuality()
        {
            var config = await _introduceConfigService.GetByKey(IntroduceKey.ServiceQuality);
            return View(config);
        }

        [Route("nha-may-cong-nghe.html")]
        public async Task<IActionResult> FactoryAndTechnology()
        {
            var config = await _introduceConfigService.GetByKey(IntroduceKey.FactoryTechnology);
            return View(config);
        }

        [Route("nguon-nhan-luc.html")]
        public async Task<IActionResult> HumanResources()
        {
            var config = await _introduceConfigService.GetByKey(IntroduceKey.HumanResource);
            return View(config);
        }
    }
}