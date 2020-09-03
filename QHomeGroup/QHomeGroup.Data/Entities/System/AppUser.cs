using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using QHomeGroup.Data.Entities.Content;
using QHomeGroup.Data.Entities.Projects;
using QHomeGroup.Data.Enum;
using QHomeGroup.Data.Interfaces;

namespace QHomeGroup.Data.Entities.System
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISwitchable
    {
        private Guid _guid;

        public AppUser()
        {
        }

        public AppUser(Guid guid, string fullName, string userName, string email, string phoneNumber, string avatar,
            Status status, DateTime? birthday, bool gender)
        {
            _guid = guid;
            FullName = fullName;
            this.UserName = userName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            Avatar = avatar;
            Status = status;
            Gender = gender;
        }

        public string FullName { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Status Status { get; set; }
        public virtual IList<Project> Projects { get; set; }
        public virtual IList<Blog> Blogs { get; set; }
        public string Token { get; set; }

        public void UpdateToken(string token)
        {
            Token = token;
        }

        public void DeleteToken()
        {
            Token = string.Empty;
        }
    }
}