using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class Session : UserOwnedEntity
    {
        [Key]
        public int UserSessionId { get; set; }

        [FullNameLength]
        public string UserName { get; set; }

        [StringLength(32)]
        public string SessionId { get; set; }

        [StringLength(32)]
        public string IPAddress { get; set; }

        [StringLength(64)]
        public string Browser { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public override int EntityKey
        {
            get { return UserSessionId; }
        }
    }
}