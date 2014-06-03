using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Services
{
    public class FilterItem
    {
        public string EditUrl
        {
            get
            {
                return Url + ItemId;
            }
        }
        public string Url { get; set; }
        public int? ItemId { get; set; }
        public string _displayText = null;
        public string DisplayText
        {
            get
            {
                string text = _displayText ?? ValueText;
                return (text.Length > 24) ? text.Substring(0, 23) + ".." : text;

            }
            set
            {
                _displayText = value;
            }
        }

        public string ValueText { get; set; }
        public int Count { get; set; }
        public string Status { get; private set; }

        public bool IsChecked
        {
            get
            {
                return Status != string.Empty;
            }
        }

        public void SetAsChecked()
        {
            Status = "checked";
        }

        public void ClearChecked()
        {
            Status = string.Empty;
        }

        public FilterItem(bool isChecked)
        {
            if (isChecked) SetAsChecked();
            else ClearChecked();
        }
    }

}