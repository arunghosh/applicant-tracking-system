using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Models
{
    public class UserCreateVm: EntityBase
    {
        public int UserId { get; set; }

        [StringLength(64)]
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [EmailAddress]
        [StringLength(128)]
        [Required]
        public string Email { get; set; }

        [StringLength(64)]
        public string Branch { get; set; }

        public UserRoleType Role { get; set; }

        public bool IsBlocked { get; set; }

        public override int EntityKey
        {
            get { return UserId; }
        }
    }
}