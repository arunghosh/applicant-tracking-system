using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Services
{
    public abstract class CandidateFilterBase : SearchFilterBase<Candidate, CandidateSearchTypes>
    {
        public CandidateFilterBase(CandidateSearchTypes type)
            : base(type)
        {

        }
    }

    public class CandidatePlaceFilter : CandidateFilterBase
    {
        public CandidatePlaceFilter()
            : base(CandidateSearchTypes.Place)
        {
            AutoComplete = Routes.AcCountry;
            Name = "Place";
            CkboxName = "_selePlce";
            IsExpanded = false;
            ShowAll = false;
        }

        public override void ComposeFilters(IEnumerable<Candidate> items)
        {
            var seleItems = items.Select(i => i.City);
            ComposeFilterItems(seleItems);
        }

        public override IEnumerable<Candidate> Execute(IEnumerable<Candidate> items)
        {
            if (!_checkedItems.Any()) return items;
            return items.Where(i => _checkedItems.Contains(i.City));
        }
    }

    public class CadicateCtgryFilter : CandidateFilterBase
    {
        public CadicateCtgryFilter()
            : base(CandidateSearchTypes.Ctgry)
        {
            //AutoComplete = Routes.AcSchool;
            Name = "Category";
            CkboxName = "_seleCtgry";
            IsExpanded = false;
            ShowAll = true;
        }

        public override void ComposeFilters(IEnumerable<Candidate> items)
        {
            var seleItems = items.Select(i => i.Category);
            ComposeFilterItems(seleItems);
        }

        public override IEnumerable<Candidate> Execute(IEnumerable<Candidate> items)
        {
            if (!_checkedItems.Any()) return items;
            return items.Where(i => _checkedItems.Contains(i.Category));
        }
    }

    public class CandiateGradeFilter : CandidateFilterBase
    {
        public CandiateGradeFilter()
            : base(CandidateSearchTypes.Grade)
        {
            //AutoComplete = Routes.AcSchool;
            Name = "Grade";
            CkboxName = "_seleGrade";
            IsExpanded = false;
            ShowAll = true;
        }

        public override void ComposeFilters(IEnumerable<Candidate> items)
        {
            var seleItems = items
                                .Select(i => i.Grade);
            ComposeFilterItems(seleItems);
        }

        public override IEnumerable<Candidate> Execute(IEnumerable<Candidate> items)
        {
            if (!_checkedItems.Any()) return items;
            return items.Where(i => _checkedItems.Contains(i.Grade));
        }
    }

    public class CandidateStatusFilter : CandidateFilterBase
    {
        public CandidateStatusFilter()
            : base(CandidateSearchTypes.Status)
        {
            //AutoComplete = Routes.AcSchool;
            Name = "Status";
            CkboxName = "_seleStat";
            IsExpanded = true;
            ShowAll = true;
            IsSort = false;
        }

        public override void ComposeFilters(IEnumerable<Candidate> items)
        {
            var seleItems = items.Select(i => i.CandidateStatus.Name);
            ComposeFilterItems(seleItems);
        }

        public override IEnumerable<Candidate> Execute(IEnumerable<Candidate> items)
        {
            if (!_checkedItems.Any()) return items;
            return items.Where(i => _checkedItems.Contains(i.CandidateStatus.Name));
        }
    }

    //public class CandidateCompanyFilter : CandidateFilterBase
    //{
    //    public CandidateCompanyFilter()
    //        : base(CandidateSearchTypes.Company)
    //    {
    //        Name = "Company";
    //        CkboxName = "_seleCpny";
    //        IsExpanded = false;
    //        ShowAll = true;
    //    }

    //    public override void ComposeFilters(IEnumerable<Candidate> items)
    //    {
    //        var seleItems = items.Select(i => i.Company);
    //        ComposeFilterItems(seleItems);
    //    }

    //    public override IEnumerable<Candidate> Execute(IEnumerable<Candidate> items)
    //    {
    //        if (!_checkedItems.Any()) return items;
    //        return items.Where(i => _checkedItems.Contains(i.Company));
    //    }
    //}

    public class CandidateAgentFilter : CandidateFilterBase
    {
        public CandidateAgentFilter()
            : base(CandidateSearchTypes.Agent)
        {
            AutoComplete = Routes.AcAgent;
            Name = "Agent";
            CkboxName = "_seleAgnt";
            IsExpanded = false;
            ShowAll = false;
        }

        public override void ComposeFilters(IEnumerable<Candidate> items)
        {
            var seleItems = items.Select(i => i.Agent);
            ComposeFilterItems(seleItems);
        }

        public override IEnumerable<Candidate> Execute(IEnumerable<Candidate> items)
        {
            if (!_checkedItems.Any()) return items;
            return items.Where(i => _checkedItems.Contains(i.Agent));
        }
    }

    public class ProjectNameFilter : CandidateFilterBase
    {
        public ProjectNameFilter()
            : base(CandidateSearchTypes.PrjName)
        {
            AutoComplete = Routes.AcProjectName;
            Name = "Project";
            CkboxName = "_selePrj";
            IsExpanded = true;
            ShowAll = false;
        }

        public override void ComposeFilters(IEnumerable<Candidate> items)
        {
            var seleItems = items.Select(i => i.Project.ProjectName);
            ComposeFilterItems(seleItems);
        }

        public override IEnumerable<Candidate> Execute(IEnumerable<Candidate> items)
        {
            if (!_checkedItems.Any()) return items;
            return items.Where(i => _checkedItems.Contains(i.Project.ProjectName));
        }
    }

    public class EmigrationCheckFilter : CandidateFilterBase
    {

        const string EC_REQ = "EC required";
        const string EC_DONE = "EC done";
        const string EC_NOT_REQ = "EC not required";
        const string EC_NOT_DONE = "EC not done";

        Dictionary<string, Func<Candidate, bool>> _intFilter = new Dictionary<string, Func<Candidate, bool>>
        {
            {EC_REQ, u => u.IsEcCheckReq},
            {EC_DONE, u => u.IsEcCheckDone},
            {EC_NOT_REQ, u => u.EcCheckNotRequired},
            {EC_NOT_DONE, u => u.EcCheckNotDone},

        };

        public EmigrationCheckFilter()
            : base(CandidateSearchTypes.EmigrationCheck)
        {
            Name = "Emigration";
            CkboxName = "_seleEmCk";
            IsExpanded = false;
            ShowAll = true;
        }

        public override void ComposeFilters(IEnumerable<Candidate> items)
        {
            MasterFilters = FilterItems;
            FilterItems = new List<FilterItem>();
            foreach (var item in _intFilter)
            {
                var fItem = new FilterItem(_checkedItems.Contains(item.Key))
                {
                    Count = items.Where(item.Value).Count(),
                    ValueText = item.Key
                };
                FilterItems.Add(fItem);
            }
        }

        public override IEnumerable<Candidate> Execute(IEnumerable<Candidate> items)
        {
            if (!_checkedItems.Any()) return items;
            foreach (var item in _intFilter)
            {
                if (_checkedItems.Contains(item.Key))
                {
                    items = items.Where(item.Value);
                }
            }
            return items;
        }
    }

    public class CandidateNameFilter : CandidateFilterBase
    {
        public CandidateNameFilter()
            : base(CandidateSearchTypes.Name)
        {
            CkboxName = "_cdntName";
            IsList = false;
        }

        public override void ComposeFilters(IEnumerable<Candidate> items)
        {
            //var seleItems = items.Select(i => i.Institution.Name);
            //ComposeFilterItems(seleItems);
        }

        public override IEnumerable<Candidate> Execute(IEnumerable<Candidate> items)
        {
            if (!_checkedItems.Any()) return items;
            var name = _checkedItems[0].ToLower();
            return items.Where(i => i.Name.ToLower().Contains(name));
        }
    }


    public class PassportFilter : CandidateFilterBase
    {
        public PassportFilter()
            : base(CandidateSearchTypes.Passport)
        {
            CkboxName = "_ppName";
            IsList = false;
        }

        public override void ComposeFilters(IEnumerable<Candidate> items)
        {
            //var seleItems = items.Select(i => i.Institution.Name);
            //ComposeFilterItems(seleItems);
        }

        public override IEnumerable<Candidate> Execute(IEnumerable<Candidate> items)
        {
            if (!_checkedItems.Any()) return items;
            var name = _checkedItems[0].ToLower();
            return items.Where(i => i.Passport.ToLower().Contains(name));
        }
    }
}