namespace TalentMatch.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string UserType { get; set; } = default!; // Employer / JobSeeker
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        public EmployerProfile? EmployerProfile { get; set; }
        public JobSeekerProfile? JobSeekerProfile { get; set; }
    }
}