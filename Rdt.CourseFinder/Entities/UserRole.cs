using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class UserRole: UserOwnedEntity
    {
        [Key]
        public int UserRoleId { get; set; }

        public UserRoleType RoleType { get; set; }

        public bool IsActive { get; set; }

        public UserRole()
        {
            IsActive = true;
        }

        public override int EntityKey
        {
            get { return UserRoleId; }
        }
    }
}