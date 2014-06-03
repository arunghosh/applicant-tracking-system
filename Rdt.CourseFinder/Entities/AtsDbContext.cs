using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Entities
{
    public class AtsDbContext : DbContext
    {
        public AtsDbContext()
            : base("AtsEntities")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateStatus> CandidateStatuses { get; set; }
        public DbSet<CandidateLog> CandidateLogs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectLog> ProjectLogs { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Import> Imports { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<MasterCategory> MasterCategories { get; set; }

        public List<Candidate> GetByStatusCompleted(int status)
        {
            var candits = Candidates.Where(c => c.CandidateStatusId == status).ToList();
            return candits;
        }

        public int GetByStatusCompletedCnt(int status)
        {
            var canditsCnt = Candidates.Where(c => c.CandidateStatusId == status).Count();
            return canditsCnt;
        }

    }
}