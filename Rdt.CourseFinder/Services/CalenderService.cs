using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Services
{
    public class CalenderSrv : ServiceBase
    {
        public List<IGrouping<DateTime, Candidate>> GetTravelling(DateTime start, DateTime end)
        {
            var candidates = _db.Candidates.Where(c => c.TravelDate != null).ToList()
                                    .Where(c => c.TravelDate.Value.Date >= start.Date && c.TravelDate.Value.Date <= end.Date)
                                    .ToList();
            var rslt = candidates.GroupBy(m => m.TravelDate.Value.Date).ToList();
            return rslt;
        }

        public List<Candidate> GetTravelling(DateTime start)
        {
            var candidates = _db.Candidates.Where(c => c.TravelDate != null).ToList()
                                    .Where(c => c.TravelDate.Value.Date == start.Date)
                                    .ToList();
            return candidates;
        }
    }
}