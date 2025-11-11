using System.Runtime.Serialization;

namespace TalentMatch.Infrastructure.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string>? Errors { get; }

        public ValidationException()
            : base("One or more validation errors have occurred.")
        {
            Errors = new List<string>();
        }

        public ValidationException(string message)
            : base(message)
        {
            Errors = new List<string>();
        }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            Errors = new List<string>();
        }

        public ValidationException(List<string> failures)
            : this()
        {
            Errors = failures;
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Errors = info.GetValue("Errors", typeof(List<string>)) as List<string>;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Errors", Errors);
        }
    }
}