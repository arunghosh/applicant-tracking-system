using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Infrastructure;

namespace Rdt.CourseFinder.Entities
{
    public class Candidate : EntityBase
    {
        [Key]
        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public int CandidateId { get; set; }

        [StringLength(128)]
        [Required]
        public string Name { get; set; }

        [StringLength(64)]
        public string Passport { get; set; }

        [StringLength(32)]
        [Display(Name = "BS(RO)")]
        public string Bsro { get; set; }

        [StringLength(32)]
        [Display(Name = "Selection Date")]
        public string SeleDate { get; set; }

        [StringLength(32)]
        public string ContactNo { get; set; }

        [StringLength(64)]
        public string Category { get; set; }

        [StringLength(64)]
        public string Experience { get; set; }

        [StringLength(10)]
        public string Age { get; set; }

        [StringLength(64)]
        public string City { get; set; }

        [StringLength(64)]
        public string Agent { get; set; }

        [StringLength(128)]
        public string Airlines { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Travel Date")]
        public DateTime? TravelDate { get; set; }

        [StringLength(64)]
        [Display(Name = "Boarding City")]
        public string BoardingCity { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Medical Done Date")]
        public DateTime? MedicalDoneDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Medical Expiry Date")]
        public DateTime? MedicalExpiryDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Visa Issued Date")]
        public DateTime? VisaIssuesDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        [Display(Name = "Visa Expiry Date")]
        public DateTime? VisaExpiryDate { get; set; }

        [Display(Name = "Visa Deposited")]
        public bool IsVisaDeposited { get; set; }

        [Display(Name = "Travel Postponement")]
        public TravelPostStates TravelPostponement { get; set; }

        [Display(Name = "EC Required")]
        [TemplatesVisibility(ShowForEdit = false)]
        public bool IsEcCheckReq { get; set; }

        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        [NotMapped]
        public bool EcCheckNotRequired { get { return !IsEcCheckReq; } }

        [Display(Name = "EC Done")]
        [TemplatesVisibility(ShowForEdit = false)]
        public bool IsEcCheckDone { get; set; }

        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        [NotMapped]
        public bool EcCheckNotDone { get { return !IsEcCheckDone; } }

        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public DateTime StatusUpdatedAt { get; set; }

        [StringLength(256)]
        public string Remarks { get; set; }

        [StringLength(64)]
        public string Grade { get; set; }

        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public int CandidateStatusId { get; set; }

        [ForeignKey("CandidateStatusId")]
        public virtual CandidateStatus CandidateStatus { get; set; }

        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public int ProjectId { get; set; }

        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public virtual Project Project { get; set; }

        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public CandidateCreateType CreationType { get; set; }

        public virtual List<CandidateLog> CandidateLogs { get; set; }

        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public int IsDeleted { get; set; }

        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public override int EntityKey
        {
            get { return CandidateId; }
        }

        public Candidate()
        {
            CandidateLogs = new List<CandidateLog>();
            IsEcCheckDone = IsEcCheckReq = IsVisaDeposited = false;
            StatusUpdatedAt = DateTime.UtcNow;
        }

        [NotMapped]
        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public bool IsMedicalReceived
        {
            get
            {
                if (CandidateStatus == null) return false;
                return CandidateStatus.CandidateStatusId >= Constants.SID_MedicalRecevied && CandidateStatus.IsPositive;
            }
        }

        [NotMapped]
        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public bool IsRectuitmentComplete
        {
            get
            {
                if (CandidateStatus == null) return false;
                return CandidateStatus.CandidateStatusId >= Constants.SID_MedicalRecevied && CandidateStatus.IsPositive;
            }
        }

        [NotMapped]
        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public bool IsDocumentSend
        {
            get
            {
                if (CandidateStatus == null) return false;
                return CandidateStatus.CandidateStatusId >= Constants.SID_DocumentsSend && CandidateStatus.IsPositive;
            }
        }

        [NotMapped]
        [TemplatesVisibility(ShowForDisplay = false, ShowForEdit = false)]
        public string EcState
        {
            get
            {
                if (IsEcCheckReq)
                {
                    return IsEcCheckDone ? "EC-D" : "EC-R";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public bool IsStatusComplete(int statusId)
        {
            if (CandidateStatus == null) return false;
            return CandidateStatus.CandidateStatusId >= statusId && CandidateStatus.IsPositive;
        }

        public void UpdateEcCheck(bool? isReq, bool? isDone)
        {
            if (isReq != null)
            {
                IsEcCheckReq = isReq ?? false;
            }
            if (isDone != null)
            {
                IsEcCheckDone = isDone ?? false;
            }
            ValidateEcCheck();
        }

        public void ValidateTravelStatus()
        {
            if (Constants.SID_Travelled == CandidateStatusId && TravelDate == null)
            {
                throw new SimpleException(string.Format("Failed to update status to 'Travelled'. Travel date not set for '{0}'", Name));
            }

            if (Constants.SID_Travelled == CandidateStatusId && TravelDate >= DateTime.Now)
            {
                throw new SimpleException(string.Format("Failed to update status to 'Travelled'. Travel date of '{0}' is not a past date.", Name));
            }
        }

        //public void ValidateVisaStatus()
        //{
        //    if (Constants.SID_VisaReceived == CandidateStatusId && TravelDate == null)
        //    {
        //        throw new SimpleException(string.Format("Failed to update status to 'Travelled'. Travel date not set for '{0}'", Name));
        //    }

        //    if (Constants.SID_Travelled == CandidateStatusId && TravelDate >= DateTime.Now)
        //    {
        //        throw new SimpleException(string.Format("Failed to update status to 'Travelled'. Travel date of '{0}' is not a past date.", Name));
        //    }
        //}

        private void ValidateEcCheck()
        {
            if (IsEcCheckDone && !IsEcCheckReq)
            {
                throw new SimpleException(string.Format("Invalid EC Status Change for {0}({1}): EC Required=false & EC Done=True", Name, Passport));
            }
        }

        public CandidateLog CreateLog(string msg, User user)
        {
            var log = new CandidateLog
            {
                ByUserId = user.UserId,
                ByUserName = user.Name,
                CandidateId = CandidateId,
                Log = msg
            };
            return log;
        }
    }
}