using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Entities;

namespace Rdt.CourseFinder.Services
{
    public class NotifyService : ServiceBase
    {
        static Dictionary<int, string> _statusMesgs = new Dictionary<int, string>()
            {
                {Constants.SID_MedicalRecevied, "Send the documents for visa processing"},
                {Constants.SID_AgentNotified, "Remind agent on the payment"} ,
                {Constants.SID_VisaReceived, "Notify the agent"} 
            };
        List<int> _statusList = new List<int>();

        public NotifyService()
        {
            var user = CurrentUser;
            switch (user.Role)
            {
                case UserRoleType.Recruiter:
                    break;
                case UserRoleType.Travel:
                    _statusList.Add(Constants.SID_VisaReceived);
                    _statusList.Add(Constants.SID_MedicalRecevied);
                    break;
                case UserRoleType.Admin:
                    break;
                case UserRoleType.Finance:
                    _statusList.Add(Constants.SID_AgentNotified);
                    break;
                default:
                    break;
            }
        }


        public List<NotifyItem> GetNotifications()
        {
            var ntfys = new List<NotifyItem>();
            foreach (var item in _statusList)
            {
                var ntfy = GetNotification(item);
                if (ntfy != null)
                {
                    ntfys.Add(ntfy);
                }
            }
            return ntfys;
        }


        private NotifyItem GetNotification(int state)
        {
            var status = DbCache.Instance.CanditStatus.First(s => s.CandidateStatusId == state);
            var cnt = _db.GetByStatusCompletedCnt(state);
            if (cnt > 0)
            {
                var model = new NotifyItem
                {
                    Count = cnt,
                    Status = status.Name,
                    Message = string.Format("There are {0} candidates in '{1}' state. \n{2} and update status.", cnt, status.Name, _statusMesgs[state])
                };
                return model;
            };
            return null;
        }

    }

    public class NotifyItem
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}