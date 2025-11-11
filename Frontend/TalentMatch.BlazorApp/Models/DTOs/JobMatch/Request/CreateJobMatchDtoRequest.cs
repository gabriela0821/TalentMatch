namespace TalentMatch.BlazorApp.Models.DTOs.JobMatch.Request
{
    public class CreateJobMatchDtoRequest
    {
        public int JobPostingId { get; set; }
        public int JobSeekerId { get; set; }
        public decimal MatchScore { get; set; }
    }
}
