using QHomeGroup.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace QHomeGroup.Application.Common.Contacts.Dtos
{
    public class ContactViewModel
    {
        public string Id { set; get; }

        [Required(ErrorMessage = "{0} bắt buộc nhập")]
        [Display(Name = "Tên")]
        public string Name { set; get; }

        [Required(ErrorMessage = "{0} bắt buộc nhập")]
        [Display(Name = "Số điện thoại")]
        public string Phone { set; get; }

        [Required(ErrorMessage = "{0} bắt buộc nhập")]
        [Display(Name = "Email")]
        public string Email { set; get; }

        [Required(ErrorMessage = "{0} bắt buộc nhập")]
        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }

        [Required(ErrorMessage = "{0} bắt buộc nhập")]
        [Display(Name = "Nội dung")]
        public string Content { set; get; }

        public Status Status { set; get; }
    }
}