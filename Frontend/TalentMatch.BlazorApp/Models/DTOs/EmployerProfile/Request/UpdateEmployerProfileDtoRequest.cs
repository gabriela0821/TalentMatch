namespace TalentMatch.BlazorApp.Models.DTOs.EmployerProfile.Request
{
    public class UpdateEmployerProfileDtoRequest
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public string City { get; set; }
        public int EmployeeCount { get; set; }
        public string Phone { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? Description { get; set; }
    }
}
