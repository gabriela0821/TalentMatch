namespace TalentMatch.Core.DTOs.User.Request
{
    public class CreateUserDtoRequest
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string UserType { get; set; } // Employer / JobSeeker
    }
}