namespace TalentMatch.BlazorApp.Models.DTOs.JobMatch.Response
{
    public class GetJobMatchDtoResponse
    {
        public int MatchId { get; set; }
        public int JobPostingId { get; set; }
        public int JobSeekerId { get; set; }
        public decimal MatchScore { get; set; }
        public string? Status { get; set; }
        public DateTime? MatchedAt { get; set; }
        public string? FullNameJobSeeker { get; set; } = null;
        public string? TitleJobPosting { get; set; } = null;
    }
}
