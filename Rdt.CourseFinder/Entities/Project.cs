using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class Project : UserOwnedEntity
    {
        [Key]
        public int ProjectId { get; set; }

        [StringLength(128)]
        [Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [NotMapped]
        [Required]
        [Display(Name = "Customer Requested Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public ProjectStatus Status { get; set; }

        [Required]
        [NotMapped]
        [Display(Name = "Target Delivery Date")]
        public DateTime DueDate { get; set; }

        public int CompanyId { get; set; }

        [StringLength(64)]
        public string HiringType { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public virtual List<Requirement> Requirements { get; set; }
        public virtual List<Milestone> Milestones { get; set; }
        public virtual List<Candidate> Candidates { get; set; }

        public virtual List<Import> Imports { get; set; }
        public List<ProjectLog> ProjetLogs { get; set; }
        public override int EntityKey
        {
            get { return ProjectId; }
        }

        [NotMapped]
        public List<string> CategorySelectList
        {
            get
            {
                var list = new List<string> { "[ -- All -- ]" };
                list = list.Concat(Candidates.Select(c => c.Category.ToLower().Trim().Replace(" ", ""))).ToList();
                list = list.Concat(Requirements.Select(r => r.Category.ToLower().Trim().Replace(" ", ""))).ToList();
                //list.ForEach(l => l.ToLower().Trim());
                list = list.Distinct().ToList();
                return list;
            }
        }

        [NotMapped]
        public List<Candidate> MedicalReceivedCandidates
        {
            get
            {
                return Candidates.Where(c => c.IsMedicalReceived).ToList();
            }
        }

        [NotMapped]
        public List<Candidate> TravelledCandidates
        {
            get
            {
                return Candidates.Where(c => c.IsStatusComplete(Constants.SID_Travelled)).ToList();
            }
        }


        [NotMapped]
        public int TotalRequirement
        {
            get
            {
                return Requirements.Sum(r => r.Count);
            }

        }

        public List<Candidate> GetByCategory(string ctgry)
        {
            ctgry = ctgry.ToLower().Trim();
            return Candidates.Where(c => c.Category.ToLower().Trim().Replace(" ", "") == ctgry).ToList();
        }

        public List<Candidate> GetByStatus(string abbr)
        {
            return Candidates.Where(c => c.CandidateStatus.Abbrevation == abbr).ToList();
        }

        public int RequirementFor(string ctgry)
        {
            ctgry = ctgry.ToLower().Trim();
            var req = Requirements.Where(c => c.Category.ToLower().Trim().Replace(" ", "") == ctgry);
            return req == null ? 0 : req.Sum(c => c.Count);
        }

        public Milestone CurrentMilestone
        {
            get
            {
                if (Milestones.Any())
                {
                    var date = DateTime.UtcNow;
                    foreach (var item in Milestones.OrderByDescending(d => d.ExpectedDate))
                    {
                        if (date > item.ExpectedDate)
                        {
                            return item;
                        }
                    }
                    return Milestones[0];
                }
                return new Milestone
                {
                    Name = "--"
                };
            }
        }


        public Project()
        {
            Status = ProjectStatus.Intial;
            ProjetLogs = new List<ProjectLog>();
            Requirements = new List<Requirement>();
            StartDate = DueDate = DateTime.UtcNow;
            Milestones = new List<Milestone>();
        }


    }
}