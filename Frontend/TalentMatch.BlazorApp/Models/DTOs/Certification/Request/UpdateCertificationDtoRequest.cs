namespace TalentMatch.BlazorApp.Models.DTOs.Certification.Request
{
    public class UpdateCertificationDtoRequest
    {
        public int Id { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string CredentialUrl { get; set; }
    }
}
