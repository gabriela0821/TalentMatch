namespace TalentMatch.Core.DTOs.User.Request
{
    public class UpdateUserDtoRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}