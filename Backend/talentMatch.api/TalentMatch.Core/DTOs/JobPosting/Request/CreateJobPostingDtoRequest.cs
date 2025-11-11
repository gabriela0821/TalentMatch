using TalentMatch.Core.DTOs.ApplicationQuestion.Request;

namespace TalentMatch.Core.DTOs.JobPosting.Request
{
    public class CreateJobPostingDtoRequest
    {
        public int EmployerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RequiredSkills { get; set; }
        public string MinEducationLevel { get; set; }
        public int MinExperience { get; set; }
        public string Location { get; set; }
        public string WorkMode { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }

        public List<CreateApplicationQuestionDtoRequest>? Questions { get; set; }
    }
}