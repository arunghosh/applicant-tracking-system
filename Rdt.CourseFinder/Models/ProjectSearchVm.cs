using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Services;
using System.Data.Entity;

namespace Rdt.CourseFinder.Models
{
    public class ProjectSearchVm : SearchVmBase<Candidate, CandidateFilterBase, CandidateSearchTypes>
    {

        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }


        public string Name
        {
            get
            {
                if (FilterMap.ContainsKey(CandidateSearchTypes.Name))
                {
                    var items = FilterMap[CandidateSearchTypes.Name].CheckedItems;
                    return items.Any() ? items[0] : string.Empty;
                }
                return string.Empty;
            }
            set
            {
                if (FilterMap.ContainsKey(CandidateSearchTypes.Name))
                {
                    FilterMap[CandidateSearchTypes.Name].CheckedItems = new List<string> { };
                    FilterMap[CandidateSearchTypes.Name].CheckedItems.Add(value ?? string.Empty);
                }
            }
        }


        public string Passport
        {
            get
            {
                if (FilterMap.ContainsKey(CandidateSearchTypes.Passport))
                {
                    var items = FilterMap[CandidateSearchTypes.Passport].CheckedItems;
                    return items.Any() ? items[0] : string.Empty;
                }
                return string.Empty;
            }
            set
            {
                if (FilterMap.ContainsKey(CandidateSearchTypes.Passport))
                {
                    FilterMap[CandidateSearchTypes.Passport].CheckedItems = new List<string> { };
                    FilterMap[CandidateSearchTypes.Passport].CheckedItems.Add(value ?? string.Empty);
                }
            }
        }

        public override bool HasFilters
        {
            get
            {
                return !string.IsNullOrEmpty(Name) || Filters.Any(f => f.FilterItems.Any(i => i.IsChecked));

            }
        }

        public override void FillMasterList()
        {
            MasterList = _db.Candidates.Where(c => c.CandidateStatus.IsPositive).Include(c => c.Project).ToList();
        }

        public override void AddFilters()
        {
            AddFilter(new PassportFilter());
            AddFilter(new CandidateNameFilter());
            AddFilter(new ProjectNameFilter());
            AddFilter(new CandidateStatusFilter());
            AddFilter(new CadicateCtgryFilter());
            AddFilter(new EmigrationCheckFilter());
            //AddFilter(new CandiateGradeFilter());
            AddFilter(new CandidateAgentFilter());
            //AddFilter(new CandidatePlaceFilter());
        }

        public ProjectSearchVm()
        {
            //if (ProjectId != 0)
            //{
            //    ProjectName = _db.Projects.Find(projectId).ProjectName;
            //}
            ItemsPerPage = 25;
        }

        public ProjectSearchVm(int? projectId)
        {
            ProjectId = projectId;
            ProjectName =  projectId == null ? "" : _db.Projects.Find(projectId).ProjectName;
            FillMasterList();
            ItemsPerPage = 25;
        }

        public IEnumerable<Candidate> TotalCandidates
        {
            get { return FilteredItems; }
        }
    }
}