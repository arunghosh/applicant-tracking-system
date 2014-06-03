using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class Milestone:EntityBase
    {
        [Key]
        public int MilestoneId { get; set; }

        [StringLength(64)]
        [Required]
        public string Name { get; set; }

        public int? CandidateStatusId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ExpectedDate { get; set; }

        public DateTime? ActualDate { get; set; }

        public MilestoneTypes MilestoneType { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        [StringLength(512)]
        public string Comments { get; set; }

        public override int EntityKey
        {
            get { return MilestoneId; }
        }

        public Milestone()
        {
            MilestoneType = MilestoneTypes.Status;
        }
    }
}