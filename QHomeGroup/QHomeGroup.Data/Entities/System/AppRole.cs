using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace QHomeGroup.Data.Entities.System
{
    [Table("AppRoles")]
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole()
        {
        }

        public AppRole(string name, string description) : base(name)
        {
            Description = description;
        }

        [StringLength(250)] public string Description { get; set; }
    }
}