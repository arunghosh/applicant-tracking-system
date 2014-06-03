using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class Import : EntityBase
    {
        [Key]
        public int ImportId { get; set; }

        public int UserId { get; set; }

        [StringLength(128)]
        public string UserName { get; set; }

        public string Path { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public int SuccessCount { get; set; }

        public int TotalCount { get; set; }

        [NotMapped]
        public int SkippedCount
        {
            get
            {
                return TotalCount - SuccessCount;
            }
        }

        [StringLength(256)]
        public string Remark { get; set; }

        public override int EntityKey
        {
            get { return ImportId; }
        }

        public Import()
        {
        }
    }
}