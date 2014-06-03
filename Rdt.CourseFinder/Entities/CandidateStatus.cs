using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder.Entities
{
    public class CandidateStatus:EntityBase
    {
        [Key]
        public int CandidateStatusId { get; set; }

        [StringLength(10)]
        [Required]
        public string Abbrevation { get; set; }

        [StringLength(32)]
        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        [Range(0, 100)]
        public int Index { get; set; }

        [Display(Name="Can be updated by")]
        public UserRoleType CanBeUpdatedBy { get; set; }

        public bool IsUpdateByAll { get; set; }

        public bool IsPositive { get; set; }

        public override int EntityKey
        {
            get { return CandidateStatusId; }
        }
    }
}