using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Services
{
    public abstract class SearchFilterBase<T, V> : FilterBase
    {
        public V SearchType { get; set; }
        public abstract void ComposeFilters(IEnumerable<T> items);
        public abstract IEnumerable<T> Execute(IEnumerable<T> items);

        public SearchFilterBase(V type)
        {
            SearchType = type;
        }
    }

    public abstract class FilterBase
    {
        protected int _dispFilterCnt = 4;
        public bool ShowAll { get; set; }
        public string Name
        {
            get;
            set;
        }
        string _displayName;
        public string DisplayName
        {
            get { return string.IsNullOrEmpty(_displayName) ? Name : _displayName; }
            set { _displayName = value; }
        }
        public string AutoComplete { get; set; }
        public string CkboxName { get; set; }
        public bool IsList { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsSort { get; set; }
        public string ExpStatus
        {
            get
            {
                return IsExpanded || CheckedItems.Any() ? "data-exp-o" : "data-exp-c";
            }
        }

        protected List<string> _checkedItems;
        public List<string> CheckedItems
        {
            get { return _checkedItems; }
            set
            {
                if (value != null && value.Any())
                {
                    value.Remove("");
                }
                _checkedItems = value;
            }
        }

        public List<FilterItem> MasterFilters { get; set; }
        public List<FilterItem> FilterItems { get; set; }

        public FilterBase()
        {
            CheckedItems = new List<string>();
            FilterItems = new List<FilterItem>();
            IsExpanded = false;
            IsList = true;
            ShowAll = false;
            IsSort = true;
        }

        protected void ComposeFilterItems(IEnumerable<string> masterList)
        {
            MasterFilters = FilterItems;
            if (masterList == null) masterList = new List<string>();
            if (_checkedItems == null) _checkedItems = new List<string>();
            var grpdItems = from item in masterList
                            group item by item into grp
                            select new FilterItem(_checkedItems.Contains(grp.Key))
                            {
                                Count = grp.Count(),
                                ValueText = grp.Key,
                            };

            var orderedList = IsSort 
                                ? grpdItems.OrderByDescending(cn => cn.Count)
                                : grpdItems;
            var finalList = orderedList.ToList();
            if (!ShowAll)
            {
                var finalquery = orderedList.Take(_dispFilterCnt);
                var checkedSkiped = orderedList.Skip(_dispFilterCnt)
                    .Where(l => l.IsChecked);
                finalquery = finalquery.Concat(checkedSkiped);
                finalList = finalquery.ToList();

            }
            var grpNames = grpdItems.Select(gi => gi.ValueText);
            foreach (var item in _checkedItems)
            {
                if (!grpNames.Contains(item))
                {
                    finalList.Add(new FilterItem(true)
                    {
                        ValueText = item,
                        Count = 0
                    });
                }
            }
            FilterItems = finalList;
        }

    }
}