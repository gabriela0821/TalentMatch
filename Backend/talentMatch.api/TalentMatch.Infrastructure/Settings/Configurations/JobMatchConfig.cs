using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Infrastructure.Settings.Configurations
{
    public class JobMatchConfig : IEntityTypeConfiguration<JobMatch>
    {
        public void Configure(EntityTypeBuilder<JobMatch> entity)
        {
            entity.ToTable("JobMatches");

            entity.HasKey(m => m.MatchId);
            entity.Property(m => m.MatchId).ValueGeneratedOnAdd();

            entity.Property(m => m.MatchScore).HasColumnType("decimal(5,2)");
            entity.Property(m => m.SkillsScore).HasColumnType("decimal(5,2)");
            entity.Property(m => m.ExperienceScore).HasColumnType("decimal(5,2)");
            entity.Property(m => m.EducationScore).HasColumnType("decimal(5,2)");
            entity.Property(m => m.LocationScore).HasColumnType("decimal(5,2)");
            entity.Property(m => m.Status).HasMaxLength(20);

            entity.HasOne(m => m.JobPosting)
                  .WithMany(j => j.JobMatches)
                  .HasForeignKey(m => m.JobPostingId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(m => m.JobSeekerProfile)
                  .WithMany(js => js.JobMatches)
                  .HasForeignKey(m => m.JobSeekerId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(m => new { m.JobPostingId, m.JobSeekerId }).IsUnique();
        }
    }
}