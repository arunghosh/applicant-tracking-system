using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public enum PassportReturnStatus
    {
        NotRequested = 0,
        PendingAdminApproval = 1,
        PendingAccountApproval = 2,
        PassportReturned = 3,
        AdminRejected = 4,
        AccountsRejected = 5,
        Approved = 6,
    }
}