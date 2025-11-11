namespace TalentMatch.Domain.Entities
{
    public class JobMatch
    {
        public int MatchId { get; set; }
        public int JobPostingId { get; set; }
        public int JobSeekerId { get; set; }
        public decimal MatchScore { get; set; }
        public decimal? SkillsScore { get; set; }
        public decimal? ExperienceScore { get; set; }
        public decimal? EducationScore { get; set; }
        public decimal? LocationScore { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime MatchedAt { get; set; }

        public JobPosting JobPosting { get; set; } = default!;
        public JobSeekerProfile JobSeekerProfile { get; set; } = default!;
    }
}