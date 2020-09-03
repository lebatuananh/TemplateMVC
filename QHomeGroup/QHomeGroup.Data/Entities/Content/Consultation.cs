using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QHomeGroup.Data.Enum;
using QHomeGroup.Data.Interfaces;
using QHomeGroup.Infrastructure.SharedKernel;

namespace QHomeGroup.Data.Entities.Content
{
    [Table("Consultations")]
    public class Consultation : DomainEntity<int>, IDateTracking
    {
        public Consultation()
        {
        }

        public Consultation(int id, string name, string email, string message, Status status) : this()
        {
            Id = id;
            Name = name;
            Email = email;
        }

        [StringLength(250)] [Required] public string Name { set; get; }

        [StringLength(250)]
        [Required]
        public string Address { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(250)] public string Email { set; get; }

        [StringLength(500)] public string Content { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
    }
}