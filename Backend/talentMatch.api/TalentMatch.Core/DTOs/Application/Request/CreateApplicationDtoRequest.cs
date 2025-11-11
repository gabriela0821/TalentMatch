namespace TalentMatch.Core.DTOs.Application.Request
{
    public class CreateApplicationDtoRequest
    {
        public int JobPostingId { get; set; }
        public int JobSeekerId { get; set; }
        public string CoverLetter { get; set; }
        public string Status { get; set; } = "Submitted";
    }
}