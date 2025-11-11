namespace TalentMatch.BlazorApp.Models.DTOs.User.Response
{
    public class GetUserDtoResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public string Token { get; set; }
    }
}
