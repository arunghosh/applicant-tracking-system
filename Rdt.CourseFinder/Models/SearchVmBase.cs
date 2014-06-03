using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder.Models
{
    public abstract class SearchVmBase<T, U, V> : ServiceBase
        where U : SearchFilterBase<T, V>
    {

        #region Paging

        public int PageIndex { get; set; }
        public int TotalPages { get; protected set; }

        #endregion

        public virtual bool HasFilters
        {
            get
            {
                return Filters.Any(f => f.FilterItems.Any(i => i.IsChecked));
            }
        }
        public int ItemsPerPage = 10;

        public IEnumerable<T> MasterList { get; set; }
        public IEnumerable<T> FilteredItems { get; set; }
        public IEnumerable<T> PagedItems
        {
            get
            {
                int itemsCnt = FilteredItems.Count();
                TotalPages = (itemsCnt / ItemsPerPage) + ((itemsCnt % ItemsPerPage == 0) ? 0 : 1);
                return FilteredItems.Skip((PageIndex - 1) * ItemsPerPage).Take(ItemsPerPage);
            }
        }
        public Dictionary<V, U> FilterMap { get; set; }
        public List<U> Filters
        {
            get
            {
                return FilterMap.Values.ToList();
            }
        }

        public SearchVmBase()
        {
            PageIndex = 1;
            FilteredItems = new List<T>();
            FilterMap = new Dictionary<V, U>();
            FillMasterList();
            AddFilters();
        }

        public void ApplyFilters(NameValueCollection form)
        {
            foreach (var item in Filters)
            {
                var req = form[item.CkboxName];
                if (!string.IsNullOrWhiteSpace(req))
                {
                    item.CheckedItems = req.Split(',').ToList();
                }
                var expStatus = form["_" + item.CkboxName];
                if (!string.IsNullOrEmpty(expStatus))
                {
                    item.IsExpanded = expStatus.ToLower() == "true" ? true : false;
                }
            }
            ApplyFilters();
        }

        public void ApplyFilters()
        {
            IEnumerable<T> items = MasterList;

            FilteredItems = items;
            ComposeFilterGrps();
            foreach (var item in Filters)
            {
                items = item.Execute(items);
            }

            FilteredItems = items;
            ComposeFilterGrps();
        }

        public abstract void FillMasterList();
        public abstract void AddFilters();

        protected void ComposeFilterGrps()
        {
            foreach (var item in Filters)
            {
                item.ComposeFilters(FilteredItems);
            }
        }

        protected void AddFilter(U grp)
        {
            FilterMap.Add(grp.SearchType, grp);
        }
    }
}