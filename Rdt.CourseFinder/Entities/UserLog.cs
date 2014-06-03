using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class UserLog : UserOwnedEntity
    {
        [Key]
        public int UserLogId { get; set; }

        [StringLength(2048)]
        public string Comment { get; set; }

        public int ByUserId { get; set; }

        [StringLength(128)]
        public string ByUserName { get; set; }

        public override int EntityKey
        {
            get { return UserLogId; }
        }

        public UserLog()
        {

        }

        public UserLog(User by, int userId)
        {
            ByUserId = by.UserId;
            ByUserName = by.Name;
            UserId = userId;
        }
    }
}