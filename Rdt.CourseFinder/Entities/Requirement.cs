using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class Requirement:EntityBase
    {
        [Key]
        public int RequirementId { get; set; }

        [StringLength(128)]
        public string Category { get; set; }
        
        [Range(0, 10000)]
        public int Count { get; set; }

        public bool IsActive { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public override int EntityKey
        {
            get { return RequirementId; }
        }
    }
}