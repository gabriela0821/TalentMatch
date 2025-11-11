using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Infrastructure.Settings.Configurations
{
    public class JobPostingConfig : IEntityTypeConfiguration<JobPosting>
    {
        public void Configure(EntityTypeBuilder<JobPosting> entity)
        {
            entity.ToTable("JobPostings");

            entity.HasKey(j => j.JobId);
            entity.Property(j => j.JobId).ValueGeneratedOnAdd();

            entity.Property(j => j.Title).IsRequired().HasMaxLength(150);
            entity.Property(j => j.Description).IsRequired();
            entity.Property(j => j.RequiredSkills).IsRequired();
            entity.Property(j => j.MinEducationLevel).HasMaxLength(50);
            entity.Property(j => j.Location).HasMaxLength(100);
            entity.Property(j => j.WorkMode).HasMaxLength(20);

            entity.HasOne(j => j.EmployerProfile)
                  .WithMany(e => e.JobPostings)
                  .HasForeignKey(j => j.EmployerId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(j => j.JobMatches)
                  .WithOne(m => m.JobPosting)
                  .HasForeignKey(m => m.JobPostingId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}