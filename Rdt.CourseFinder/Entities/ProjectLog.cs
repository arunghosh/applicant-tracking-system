using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class ProjectLog:EntityBase
    {
        [Key]
        public int ProjectLogId { get; set; }

        public int UserId { get; set; }

        [StringLength(128)]
        public string UserName { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        [StringLength(512)]
        public string Log { get; set; }


        public override int EntityKey
        {
            get { return ProjectLogId; }
        }
    }
}