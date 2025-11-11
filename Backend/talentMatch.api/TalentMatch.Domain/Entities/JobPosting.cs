namespace TalentMatch.Domain.Entities
{
    public class JobPosting
    {
        public int JobId { get; set; }
        public int EmployerId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string RequiredSkills { get; set; } = default!;
        public string MinEducationLevel { get; set; } = default!;
        public int MinExperience { get; set; }
        public string Location { get; set; } = default!;
        public string WorkMode { get; set; } = default!;
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime PostedDate { get; set; }

        public EmployerProfile EmployerProfile { get; set; } = default!;
        public List<ApplicationQuestion> ApplicationQuestions { get; set; } = new();
        public List<Application> Applications { get; set; } = new();
        public ICollection<JobMatch> JobMatches { get; set; } = new List<JobMatch>();
    }
}