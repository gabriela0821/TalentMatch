namespace TalentMatch.Core.DTOs.ApplicationAnswer.Request
{
    public class CreateApplicationAnswerDtoRequest
    {
        public int QuestionId { get; set; }
        public int JobSeekerId { get; set; }
        public string AnswerText { get; set; }
    }
}