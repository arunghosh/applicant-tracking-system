using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Services
{
    public class ReportSrv : ServiceBase
    {

        public List<Candidate> GetTravelling(DateTime start)
        {
            var candidates = _db.Candidates.Where(c => c.TravelDate != null).ToList()
                                    .Where(c => c.TravelDate.Value.Date == start.Date)
                                    .ToList();
            return candidates;
        }

        public List<Candidate> GetTravelling(DateTime start, DateTime end)
        {
            return _db.Candidates.Where(c => c.TravelDate != null).ToList()
                            .Where(c => c.TravelDate.Value.Date >= start.Date && c.TravelDate.Value.Date <= end.Date)
                            .ToList();
        }


        public List<Candidate> GetByStatus(DateTime start, DateTime end, int statusId)
        {
            return _db.Candidates.Where(c => c.TravelDate != null).ToList()
                            .Where(c => c.StatusUpdatedAt.Date >= start.Date && c.StatusUpdatedAt.Date <= end.Date && c.CandidateStatusId == statusId)
                            .ToList();
        }


    }
}