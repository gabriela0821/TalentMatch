namespace TalentMatch.Domain.Entities
{
    public class JobSeekerProfile
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; } = default!;
        public int Age { get; set; }
        public string Phone { get; set; } = default!;
        public string City { get; set; } = default!;
        public string EducationLevel { get; set; } = default!;
        public int YearsOfExperience { get; set; }
        public string Skills { get; set; } = default!;
        public decimal ExpectedSalary { get; set; }
        public string PreferredLocation { get; set; } = default!;
        public string? Summary { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = default!;
        public List<WorkExperience> WorkExperiences { get; set; } = new();
        public List<Certification> Certifications { get; set; } = new();
        public List<Application> Applications { get; set; } = new();
        public ICollection<JobMatch> JobMatches { get; set; } = new List<JobMatch>();
    }
}