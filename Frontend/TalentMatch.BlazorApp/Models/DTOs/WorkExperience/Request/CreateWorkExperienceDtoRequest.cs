namespace TalentMatch.BlazorApp.Models.DTOs.WorkExperience.Request
{
    public class CreateWorkExperienceDtoRequest
    {
        public int JobSeekerId { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentJob { get; set; }
        public string Description { get; set; }
    }
}
