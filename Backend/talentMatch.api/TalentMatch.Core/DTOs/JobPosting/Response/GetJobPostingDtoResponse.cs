namespace TalentMatch.Core.DTOs.JobPosting.Response
{
    public class GetJobPostingDtoResponse
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string RequiredSkills { get; set; } 
        public string Location { get; set; }
        public string WorkMode { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public bool IsActive { get; set; } 
        public DateTime PostedDate { get; set; }
    }
}