namespace TalentMatch.Core.DTOs.WorkExperience.Request
{
    public class UpdateWorkExperienceDtoRequest
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public bool IsCurrentJob { get; set; }
        public string Description { get; set; }
    }
}