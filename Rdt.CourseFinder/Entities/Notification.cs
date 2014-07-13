using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class Notification: EntityBase
    {
        [Key]
        public int NotificationId { get; set; }

        public override int EntityKey
        {
            get { return NotificationId; }
        }

        public int CandidateId { get; set; }

        [FullNameLength]
        public string CandidateName { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [FullNameLength]
        public string UserName { get; set; }

        public UserRoleType UserRole { get; set; }

        [StringLength(512)]
        public string Message { get; set; }

        public bool IsRead { get; set; }

        public bool IsDeleted { get; set; }
    }
}