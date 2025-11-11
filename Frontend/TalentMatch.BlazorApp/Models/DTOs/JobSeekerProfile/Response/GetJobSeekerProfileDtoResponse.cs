namespace TalentMatch.BlazorApp.Models.DTOs.JobSeekerProfile.Response
{
    public class GetJobSeekerProfileDtoResponse
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; } = default!;
        public string City { get; set; }
        public string EducationLevel { get; set; }
        public int YearsOfExperience { get; set; }
        public string Skills { get; set; }
        public decimal ExpectedSalary { get; set; }
        public string PreferredLocation { get; set; }
        public string? Summary { get; set; }
    }
}
