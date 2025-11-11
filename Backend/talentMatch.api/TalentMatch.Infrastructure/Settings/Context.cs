using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Infrastructure.Settings
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<EmployerProfile> EmployerProfiles { get; set; }
        public virtual DbSet<JobSeekerProfile> JobSeekerProfiles { get; set; }
        public virtual DbSet<WorkExperience> WorkExperiences { get; set; }
        public virtual DbSet<Certification> Certifications { get; set; }
        public virtual DbSet<JobMatch> JobMatches { get; set; }
        public virtual DbSet<JobPosting> JobPostings { get; set; }
        public virtual DbSet<ApplicationQuestion> ApplicationQuestions { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationAnswer> ApplicationAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}