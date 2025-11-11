using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Infrastructure.Settings.Configurations
{
    public class ApplicationAnswerConfig : IEntityTypeConfiguration<ApplicationAnswer>
    {
        public void Configure(EntityTypeBuilder<ApplicationAnswer> entity)
        {
            entity.ToTable("ApplicationAnswers");

            entity.HasKey(a => a.AnswerId);
            entity.Property(a => a.AnswerId).ValueGeneratedOnAdd();

            entity.Property(a => a.AnswerText).IsRequired();

            entity.HasOne(a => a.ApplicationQuestion)
                  .WithMany(q => q.ApplicationAnswers)
                  .HasForeignKey(a => a.QuestionId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.JobSeekerProfile)
                  .WithMany()
                  .HasForeignKey(a => a.JobSeekerId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(a => new { a.QuestionId, a.JobSeekerId }).IsUnique();
        }
    }
}