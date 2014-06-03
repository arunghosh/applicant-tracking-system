using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Models
{
    public class CalenderVm
    {
        public int MaxCnt
        {
            get
            {
                var max = 0;
                if (Result != null)
                {
                    foreach (var item in Result)
                    {
                        max = item.Count() > max ? item.Count() : max;
                    }
                }
                return max;
            }
        }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }
        public List<IGrouping<DateTime, Candidate>> Result { get; set; }

        public CalenderVm()
        {
            StartDate = DateTime.UtcNow;
            EndDate = StartDate.AddDays(14);
        }
    }
}