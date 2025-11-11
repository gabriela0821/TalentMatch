namespace TalentMatch.Core.Wrappers
{
    public class Response<T>
    {
        public T? Data { get; set; }

        public bool? Succeeded { get; set; }

        public List<string>? Errors { get; set; }

        public string? CodeError { get; set; }

        public string? Message { get; set; }

        public Response(T data)
        {
            Succeeded = true;
            CodeError = string.Empty;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }

        public Response(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public Response(T data, bool succeeded, List<string> errors, string codeError, string message)
            : this(data)
        {
            Succeeded = succeeded;
            Errors = errors;
            CodeError = codeError;
            Message = message;
        }

        public Response()
        {
        }
    }
}