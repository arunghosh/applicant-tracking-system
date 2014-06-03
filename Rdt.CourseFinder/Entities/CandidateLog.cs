using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class CandidateLog:EntityBase
    {
        [Key]
        public int CandidateLogId { get; set; }

        public int ByUserId { get; set; }

        [StringLength(128)]
        public string ByUserName { get; set; }

        public int CandidateId { get; set; }

        [ForeignKey("CandidateId")]
        public Candidate Candidate { get; set; }

        [StringLength(512)]
        public string Log { get; set; }

        public override int EntityKey
        {
            get { return CandidateLogId; }
        }
    }
}