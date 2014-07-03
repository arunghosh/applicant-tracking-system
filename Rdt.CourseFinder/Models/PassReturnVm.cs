using Rdt.CourseFinder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Models
{
    public class PassReturnVm
    {
        public List<Candidate> Candidates { get; set; }

        public byte AgreeStatus { get; set; }

        public byte RejectStatus { get; set; }
    }
}