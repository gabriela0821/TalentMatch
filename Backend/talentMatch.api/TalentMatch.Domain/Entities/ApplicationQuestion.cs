namespace TalentMatch.Domain.Entities
{
    public class ApplicationQuestion
    {
        public int QuestionId { get; set; }
        public int JobPostingId { get; set; }
        public string QuestionText { get; set; } = default!;
        public string QuestionType { get; set; } = default!; // Text, YesNo, File
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; }

        public JobPosting JobPosting { get; set; } = default!;
        public List<ApplicationAnswer> ApplicationAnswers { get; set; } = new();
    }
}