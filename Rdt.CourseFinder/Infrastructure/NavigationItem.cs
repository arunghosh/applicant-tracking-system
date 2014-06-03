using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder
{
    public class NavigationItem
    {
        public List<NavigationItem> NavivationItems { get; set; }

        public string DisplayText { get; set; }
        public string RoutingText
        {
            get
            {
                return PageType.ToString();
            }
        }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public PageTypes PageType { get; set; }
        public string Titile { get; set; }
        public string ImageUrl { get; set; }
        public string IconName { get; set; }

        public string Url
        {
            get
            {
                return string.Format(@"\{0}\{1}\{2}", Area, Controller, Action);
            }
        }

        public string TinyUrl
        {
            get { return "/" + RoutingText; }
        }

        public NavigationItem()
        {
            NavivationItems = new List<NavigationItem>();
        }


        public NavigationItem Clone()
        {
            return new NavigationItem
            {
                Action = Action,
                Area = Area,
                Controller = Controller,
                Titile = Titile,
                DisplayText = DisplayText,
                IconName = IconName,
                ImageUrl = ImageUrl,
                PageType = PageType,
            };
        }
    }

}