namespace TalentMatch.Domain.Entities
{
    public class WorkExperience
    {
        public int ExperienceId { get; set; }
        public int JobSeekerId { get; set; }
        public string CompanyName { get; set; } = default!;
        public string JobTitle { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentJob { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public JobSeekerProfile JobSeekerProfile { get; set; } = default!;
    }
}