namespace TalentMatch.Core.DTOs.Certification.Request
{
    public class CreateCertificationDtoRequest
    {
        public int JobSeekerId { get; set; }
        public string CertificationName { get; set; }
        public string IssuingOrganization { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string CredentialUrl { get; set; }
    }
}