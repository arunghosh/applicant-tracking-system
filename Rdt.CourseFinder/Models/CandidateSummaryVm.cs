using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Models
{
    public class CandidateSummaryVm
    {
        public int RequiredCnt { get; set; }
        public List<Candidate> Candidates { get; set; }
        public IEnumerable<IGrouping<string, string>> Status { get; set; }
        public IEnumerable<IGrouping<string, string>> CandidateGroups { get; set; }
        public CandidateSummaryVm(Project prj)
        {
            Candidates = prj.Candidates;
            RequiredCnt = prj.TotalRequirement;
            Init();
        }
        public CandidateSummaryVm(List<Candidate> candidates)
        {
            Candidates = candidates;
            Init();
        }
        void Init()
        {
            CandidateGroups = Candidates.GroupBy(m => m.Category, m => m.CandidateStatus.Abbrevation);
            Status = Candidates.GroupBy(m => m.CandidateStatus.Abbrevation, m => m.Category);
        }
    }
}