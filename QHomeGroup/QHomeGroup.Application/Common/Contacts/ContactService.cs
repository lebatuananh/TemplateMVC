using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QHomeGroup.Application.Common.Contacts.Dtos;
using QHomeGroup.Application.Notification;
using QHomeGroup.Data.Entities.Content;
using QHomeGroup.Data.Entities.System;
using QHomeGroup.Infrastructure.Interfaces;
using QHomeGroup.Utilities.Constants;
using QHomeGroup.Utilities.Dtos;
using QHomeGroup.Utilities.Extensions;

namespace QHomeGroup.Application.Common.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact, string> _contactRepository;
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public ContactService(IRepository<Contact, string> contactRepository, INotificationService notificationService, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _contactRepository = contactRepository;
            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task Delete(string id)
        {
            var contact = await _contactRepository.FindByIdAsync(id);
            _contactRepository.Remove(contact);
        }

        public async Task<IList<ContactViewModel>> GetAll()
        {
            return (await _contactRepository.GetAllAsync()).To<IList<ContactViewModel>>();
        }

        public async Task<QueryResult<ContactViewModel>> GetAllPaging(string keyword, int skip, int take)
        {
            var queryResult = await _contactRepository.QueryAsync(t => string.IsNullOrEmpty(keyword) || EF.Functions.Like(t.Name, $"%{keyword}%"), skip, take);
            return queryResult.To<QueryResult<ContactViewModel>>();
        }

        public async Task<ContactViewModel> GetById(string id)
        {
            return (await _contactRepository.FindByIdAsync(id)).To<ContactViewModel>();
        }

        public async Task Create(ContactViewModel contactVm)
        {
            var contact = new Contact(contactVm.Name, contactVm.Phone, contactVm.Email, contactVm.Address, contactVm.Content, contactVm.Status);
            _contactRepository.Add(contact);
            await _unitOfWork.SaveChangesAsync();
            var admin = await _userManager.FindByNameAsync("admin");
            if (admin != null)
            {
                await _notificationService.SendToDevice(admin.Token, @"Có liên hệ mới", $"{contactVm.Name} đã để lại liên hệ", contact.Id);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ExportContactData> Export()
        {
            var contacts = await _contactRepository.GetAllAsync();
            var memory = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memory))
            {
                var workSheet = excel.Workbook.Worksheets.Add("Contact Summary");
                workSheet.Cells["A3"].Value = "Danh sách khách hàng đã đăng ký";
                workSheet.Cells["A3"].AutoFitColumns();
                workSheet.Cells["A3:D3"].Merge = true;
                workSheet.Cells["A3:D3"].Style.Font.Size = 16;
                workSheet.Cells["A3:D3"].Style.Font.Bold = true;
                workSheet.Cells["A5"].Value = "Họ & tên";
                workSheet.Cells["A5"].Style.Font.Bold = true;
                workSheet.Cells["B5"].Value = "Số điện thoại";
                workSheet.Cells["B5"].Style.Font.Bold = true;
                workSheet.Cells["C5"].Value = "Email";
                workSheet.Cells["C5"].Style.Font.Bold = true;
                workSheet.Cells["D5"].Value = "Địa chỉ căn hộ";
                workSheet.Cells["D5"].Style.Font.Bold = true;
                workSheet.Cells["E5"].Value = "Nội dung";
                workSheet.Cells["E5"].Style.Font.Bold = true;
                int rowNumber = 6;
                foreach (var contact in contacts)
                {
                    workSheet.Cells[rowNumber, 1].Value = contact.Name;
                    workSheet.Cells[rowNumber, 2].Value = contact.Phone;
                    workSheet.Cells[rowNumber, 3].Value = contact.Email;
                    workSheet.Cells[rowNumber, 4].Value = contact.Address;
                    workSheet.Cells[rowNumber, 5].Value = contact.Content;
                    rowNumber++;
                }
                excel.Save();
            }
            var fileName = "Contact Summary " + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
            return new ExportContactData
            {
                Data = memory,
                FileName = fileName
            };
        }
    }
}