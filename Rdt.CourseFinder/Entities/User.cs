using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Models;

namespace Rdt.CourseFinder.Entities
{
    public class User: EntityBase
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(256)]
        [Required]
        public string Password { get; set; }

        [StringLength(64)]
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        [EmailAddress]
        [StringLength(128)]
        [Required]
        public string Email { get; set; }

        [Phone]
        [StringLength(32)]
        public string Mobile { get; set; }

        public virtual List<Session> Sessions { get; set; }
        public virtual List<UserRole> Roles { get; set; }
        public virtual List<UserLog> UserLogs { get; set; }

        public UserRoleType Role { get; set; }

        public bool IsBlocked { get; set; }

        public User()
        {
            Role = UserRoleType.Recruiter;
        }

        public override int EntityKey
        {
            get { return UserId; }
        }

        public UserLog CreateLog(string msg)
        {
            var log = new UserLog
            {
                UserId = UserId,
                ByUserName = Name,
                ByUserId = UserId,
                Comment = msg,
            };
            return log;
        }

        public void UpdateUserCreateVm(UserCreateVm model)
        {
            Email = model.Email;
            Name = model.Name;
            Role = model.Role;
            IsBlocked = model.IsBlocked;
        }
        public UserCreateVm GetUserCreateVm()
        {
            return new UserCreateVm
            {
                Email = Email,
                Name = Name,
                Role = Role,
                UserId = UserId,
                IsBlocked = IsBlocked
            };
        }
    }
}