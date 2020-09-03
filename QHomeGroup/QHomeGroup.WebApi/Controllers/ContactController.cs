using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using QHomeGroup.Application.Common.Contacts;

namespace QHomeGroup.WebApi.Controllers
{
    [Authorize]
    public class ContactController : V1Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Query(string query, int skip = 0, int take = 10)
        {
            var contacts = await _contactService.GetAllPaging(query, skip, take);
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var contact = await _contactService.GetById(id);
            return Ok(contact);
        }

        [HttpPost("export")]
        public async Task<IActionResult> Export()
        {
            var result = await _contactService.Export();
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            var contentDispositionHeaderValue = new ContentDispositionHeaderValue("attachment");
            contentDispositionHeaderValue.SetHttpFileName(result.FileName);
            var str = contentDispositionHeaderValue.ToString();
            Response.Headers.Append("Content-Disposition", str);
            return File(result.Data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}