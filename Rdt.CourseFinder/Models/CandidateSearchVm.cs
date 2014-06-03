using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Services;
using System.Data.Entity;

namespace Rdt.CourseFinder.Models
{
    public class CandidateSearchVm : SearchVmBase<Candidate, CandidateFilterBase, CandidateSearchTypes>
    {

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
            MasterList = _db.Candidates.ToList();
        }

        public override void AddFilters()
        {
            AddFilter(new PassportFilter());
            AddFilter(new CandidateNameFilter());
            AddFilter(new ProjectNameFilter());
            AddFilter(new CadicateCtgryFilter());
            AddFilter(new CandidatePlaceFilter());
            AddFilter(new CandidateStatusFilter());
            //AddFilter(new CandiateGradeFilter());
            //AddFilter(new CandidateAgentFilter());
        }

        public IEnumerable<Candidate> TotalCandidates
        {
            get { return FilteredItems; }
        }
    }
}