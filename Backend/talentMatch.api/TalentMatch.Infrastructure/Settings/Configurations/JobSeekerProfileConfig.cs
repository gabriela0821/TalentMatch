using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Infrastructure.Settings.Configurations
{
    public class JobSeekerProfileConfig : IEntityTypeConfiguration<JobSeekerProfile>
    {
        public void Configure(EntityTypeBuilder<JobSeekerProfile> entity)
        {
            entity.ToTable("JobSeekerProfiles");

            entity.HasKey(x => x.ProfileId);
            entity.Property(x => x.ProfileId).ValueGeneratedOnAdd();

            entity.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Phone).HasMaxLength(20);
            entity.Property(x => x.City).HasMaxLength(100);
            entity.Property(x => x.EducationLevel).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Skills).IsRequired();
            entity.Property(x => x.PreferredLocation).HasMaxLength(100);
            entity.Property(x => x.Summary).HasMaxLength(500);
            entity.Property(x => x.ExpectedSalary).HasColumnType("decimal(18,2)");

            entity.HasOne(x => x.User)
                  .WithOne(u => u.JobSeekerProfile)
                  .HasForeignKey<JobSeekerProfile>(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(js => js.JobMatches)
                   .WithOne(m => m.JobSeekerProfile)
                   .HasForeignKey(m => m.JobSeekerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}