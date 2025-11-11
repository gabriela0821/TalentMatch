namespace TalentMatch.Domain.Entities
{
    public class ApplicationAnswer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int JobSeekerId { get; set; }
        public string AnswerText { get; set; } = default!;
        public DateTime AnsweredAt { get; set; }

        public ApplicationQuestion ApplicationQuestion { get; set; } = default!;
        public JobSeekerProfile JobSeekerProfile { get; set; } = default!;
    }
}