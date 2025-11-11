namespace TalentMatch.Domain.Entities
{
    public class Certification
    {
        public int CertificationId { get; set; }
        public int JobSeekerId { get; set; }
        public string CertificationName { get; set; } = default!;
        public string IssuingOrganization { get; set; } = default!;
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? CredentialUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public JobSeekerProfile JobSeekerProfile { get; set; } = default!;
    }
}