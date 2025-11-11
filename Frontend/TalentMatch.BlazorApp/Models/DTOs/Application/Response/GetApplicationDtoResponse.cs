namespace TalentMatch.BlazorApp.Models.DTOs.Application.Response
{
    public class GetApplicationDtoResponse
    {
        public int ApplicationId { get; set; }
        public int JobPostingId { get; set; }
        public int JobSeekerId { get; set; }
        public string? CoverLetter { get; set; }
        public string Status { get; set; }
        public DateTime AppliedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
