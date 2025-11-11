using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Infrastructure.Settings.Configurations
{
    public class EmployerProfileConfig : IEntityTypeConfiguration<EmployerProfile>
    {
        public void Configure(EntityTypeBuilder<EmployerProfile> entity)
        {
            entity.ToTable("EmployerProfiles");

            entity.HasKey(x => x.ProfileId);
            entity.Property(x => x.ProfileId).ValueGeneratedOnAdd();

            entity.Property(x => x.CompanyName).IsRequired().HasMaxLength(150);
            entity.Property(x => x.Industry).IsRequired().HasMaxLength(100);
            entity.Property(x => x.City).HasMaxLength(100);
            entity.Property(x => x.Phone).HasMaxLength(20);
            entity.Property(x => x.WebsiteUrl).HasMaxLength(255);
            entity.Property(x => x.Description).HasMaxLength(1000);

            entity.HasOne(x => x.User)
                  .WithOne(u => u.EmployerProfile)
                  .HasForeignKey<EmployerProfile>(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}