using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Infrastructure.Settings.Configurations
{
    public class WorkExperienceConfig : IEntityTypeConfiguration<WorkExperience>
    {
        public void Configure(EntityTypeBuilder<WorkExperience> entity)
        {
            entity.ToTable("WorkExperience");

            entity.HasKey(w => w.ExperienceId);
            entity.Property(w => w.ExperienceId).ValueGeneratedOnAdd();

            entity.Property(w => w.CompanyName).IsRequired().HasMaxLength(150);
            entity.Property(w => w.JobTitle).IsRequired().HasMaxLength(100);
            entity.Property(w => w.Description).HasMaxLength(500);

            entity.HasOne(w => w.JobSeekerProfile)
                  .WithMany(js => js.WorkExperiences)
                  .HasForeignKey(w => w.JobSeekerId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}