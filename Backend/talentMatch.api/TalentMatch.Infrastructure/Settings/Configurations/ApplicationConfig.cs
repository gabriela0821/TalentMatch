using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Infrastructure.Settings.Configurations
{
    public class ApplicationConfig : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> entity)
        {
            entity.ToTable("Applications");

            entity.HasKey(a => a.ApplicationId);
            entity.Property(a => a.ApplicationId).ValueGeneratedOnAdd();

            entity.Property(a => a.Status).IsRequired().HasMaxLength(20);
            entity.Property(a => a.CoverLetter);

            entity.HasOne(a => a.JobPosting)
                  .WithMany(j => j.Applications)
                  .HasForeignKey(a => a.JobPostingId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.JobSeekerProfile)
                  .WithMany(js => js.Applications)
                  .HasForeignKey(a => a.JobSeekerId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(a => new { a.JobPostingId, a.JobSeekerId }).IsUnique();
        }
    }
}