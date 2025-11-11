namespace TalentMatch.Domain.Entities
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public int JobPostingId { get; set; }
        public int JobSeekerId { get; set; }
        public string? CoverLetter { get; set; }
        public string Status { get; set; } = "Submitted"; // Submitted / Reviewed / Rejected / Accepted
        public DateTime AppliedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public JobPosting JobPosting { get; set; } = default!;
        public JobSeekerProfile JobSeekerProfile { get; set; } = default!;
    }
}