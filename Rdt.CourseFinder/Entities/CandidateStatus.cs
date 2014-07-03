using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rdt.CourseFinder.Services;

namespace Rdt.CourseFinder.Entities
{
    public class CandidateStatus:EntityBase
    {
        [Key]
        public int CandidateStatusId { get; set; }

        [StringLength(10)]
        [Required]
        public string Abbrevation { get; set; }

        [StringLength(32)]
        [Required]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        [Range(0, 100)]
        public int Index { get; set; }

        [Display(Name="Can be updated by")]
        public UserRoleType CanBeUpdatedBy { get; set; }

        public bool IsUpdateByAll { get; set; }

        public bool IsPositive { get; set; }

        public override int EntityKey
        {
            get { return CandidateStatusId; }
        }

        private static int _travelledIndex = -1;
        public static int TravelledIndex
        {
            get 
            {
                if (_travelledIndex == -1)
                {
                    _travelledIndex = DbCache.Instance.CanditStatus.Single(c => c.CandidateStatusId == Constants.SID_Travelled).Index;
                }
                return _travelledIndex; 
            }
        }

        private static int _medRxIndex = -1;
        public static int MedRxIndex
        {
            get
            {
                if (_medRxIndex == -1)
                {
                    _medRxIndex = DbCache.Instance.CanditStatus.Single(c => c.CandidateStatusId == Constants.SID_MedicalRecevied).Index;
                }
                return _medRxIndex;
            }
        }

        public int IndexOf(int statusId)
        {
            return DbCache.Instance.CanditStatus.Single(c => c.CandidateStatusId == statusId).Index;
        }
    }
}