namespace TalentMatch.Core.DTOs.ApplicationQuestion.Request
{
    public class CreateApplicationQuestionDtoRequest
    {
        public int JobPostingId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
    }
}