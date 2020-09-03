using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QHomeGroup.Data.Enum;
using QHomeGroup.Infrastructure.SharedKernel;

namespace QHomeGroup.Data.Entities.Content
{
    [Table("Contacts")]
    public class Contact : DomainEntity<string>
    {
        public Contact()
        {
        }
        [StringLength(250)] [Required] public string Name { set; get; }

        [StringLength(50)] public string Phone { set; get; }

        [StringLength(250)] public string Email { set; get; }

        [StringLength(250)] public string Address { set; get; }

        public string Content { get; set; }

        public Status Status { get; set; }

        public Contact(string name, string phone, string email, string address, string content, Status status)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            Content = content;
            Status = status;
        }
    }
}