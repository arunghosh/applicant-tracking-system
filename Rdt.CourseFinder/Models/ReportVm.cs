using Rdt.CourseFinder.Entities;
using Rdt.CourseFinder.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Models
{
    public class ReportBaseVm
    {

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }

        public List<Candidate> Candidates { get; set; }

        public Dictionary<string, List<CompanyTravelCnt>> ResultByHiringType
        {
            get
            {
                var byHiringTypeGrp = Candidates.GroupBy(m => m.Project.HiringType).ToList();
                var result = new Dictionary<string, List<CompanyTravelCnt>>();
                foreach (var hringGrp in byHiringTypeGrp)
                {
                    var byPrjNameGrp = hringGrp.GroupBy(c => c.Project.Company.CompanyName);
                    var trvlCnts = new List<CompanyTravelCnt>();
                    foreach (var prjGrp in byPrjNameGrp)
                    {
                        var trvlCnt = new CompanyTravelCnt
                        {
                            Count = prjGrp.Count(),
                            CompanyName = prjGrp.Key
                        };
                        trvlCnts.Add(trvlCnt);
                    }
                    result.Add(hringGrp.Key, trvlCnts);
                }
                return result;
            }
        }

        public ReportBaseVm()
        {
            StartDate = DateTime.UtcNow;
            EndDate = StartDate.AddDays(14);
            Candidates = new List<Candidate>();
        }
    }

    public class ReportVm : ReportBaseVm
    {
        ReportSrv _srv = new ReportSrv();

        public List<ReportStatusMap> ReportStatusMaps = new List<ReportStatusMap>
        {
            new ReportStatusMap(Constants.SID_Travelled, "Travelled"),
            new ReportStatusMap(Constants.SID_Cancelled, "Cancelled"),
        };

        public int SelectedStatusId { get; set; }

        public string StatusName
        {
            get
            {
                return ReportStatusMaps.Single(m => m.StatusId == SelectedStatusId).StatusDisplay;
            }
        }

        public ReportVm()
        {
            SelectedStatusId = 0;
        }

        public void RefreshCandidates()
        {
            Candidates = _srv.GetByStatus(StartDate, EndDate, SelectedStatusId);
        }

    }

    public class ReportStatusMap
    {
        public int StatusId { get; set; }
        public string StatusDisplay { get; set; }

        public ReportStatusMap(int id, string name)
        {
            StatusId = id;
            StatusDisplay = name;
        }
    }

    public class CompanyTravelCnt
    {
        public int Count { get; set; }
        public string CompanyName { get; set; }
    }
}