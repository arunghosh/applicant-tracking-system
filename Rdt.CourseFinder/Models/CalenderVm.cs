using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder.Models
{
    public class CalenderVm: ReportBaseVm
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

        public List<IGrouping<DateTime, Candidate>> Result
        {
            get
            {
                var rslt = Candidates.GroupBy(m => m.TravelDate.Value.Date).ToList();
                return rslt;
            }
        }
    }
}