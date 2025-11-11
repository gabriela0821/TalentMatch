namespace TalentMatch.Domain.Entities
{
    public class EmployerProfile
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; } = default!;
        public string Industry { get; set; } = default!;
        public string City { get; set; } = default!;
        public int EmployeeCount { get; set; }
        public string Phone { get; set; } = default!;
        public string? WebsiteUrl { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = default!;
        public List<JobPosting> JobPostings { get; set; } = new();
    }
}