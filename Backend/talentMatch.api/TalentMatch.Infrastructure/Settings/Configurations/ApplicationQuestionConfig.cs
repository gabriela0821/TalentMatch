using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Infrastructure.Settings.Configurations
{
    public class ApplicationQuestionConfig : IEntityTypeConfiguration<ApplicationQuestion>
    {
        public void Configure(EntityTypeBuilder<ApplicationQuestion> entity)
        {
            entity.ToTable("ApplicationQuestions");

            entity.HasKey(q => q.QuestionId);
            entity.Property(q => q.QuestionId).ValueGeneratedOnAdd();

            entity.Property(q => q.QuestionText).IsRequired().HasMaxLength(500);
            entity.Property(q => q.QuestionType).HasMaxLength(20);
            entity.Property(q => q.DisplayOrder);

            entity.HasOne(q => q.JobPosting)
                  .WithMany(j => j.ApplicationQuestions)
                  .HasForeignKey(q => q.JobPostingId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}