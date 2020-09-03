using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QHomeGroup.Application.Common.Contacts;
using QHomeGroup.Application.Common.Contacts.Dtos;
using QHomeGroup.Application.Common.Contacts.Requests;
using QHomeGroup.Application.Introduce;

namespace QHomeGroup.WebApplication.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ICompanyIntroService _companyIntroService;

        public ContactController(IContactService contactService, ICompanyIntroService companyIntroService)
        {
            _contactService = contactService;
            _companyIntroService = companyIntroService;
        }

        [Route("lien-he.html")]
        public async Task<IActionResult> Index()
        {
            var companyIntro = await _companyIntroService.Get();
            ViewData["company"] = companyIntro;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("lien-he.html")]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            var companyIntro = await _companyIntroService.Get();
            ViewData["company"] = companyIntro;
            if (!ModelState.IsValid)
                return View();
            await _contactService.Create(model);
            ViewData["Success"] = "Đã gửi liên hệ. Chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất";
            return View();
        }
    }
}