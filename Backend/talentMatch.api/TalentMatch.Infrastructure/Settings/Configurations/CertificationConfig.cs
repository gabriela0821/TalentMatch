using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Infrastructure.Settings.Configurations
{
    public class CertificationConfig : IEntityTypeConfiguration<Certification>
    {
        public void Configure(EntityTypeBuilder<Certification> entity)
        {
            entity.ToTable("Certifications");

            entity.HasKey(c => c.CertificationId);
            entity.Property(c => c.CertificationId).ValueGeneratedOnAdd();

            entity.Property(c => c.CertificationName).IsRequired().HasMaxLength(150);
            entity.Property(c => c.IssuingOrganization).HasMaxLength(150);
            entity.Property(c => c.CredentialUrl).HasMaxLength(255);

            entity.HasOne(c => c.JobSeekerProfile)
                  .WithMany(js => js.Certifications)
                  .HasForeignKey(c => c.JobSeekerId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}