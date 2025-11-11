using System.Globalization;
using System.Runtime.Serialization;

namespace TalentMatch.Infrastructure.Exceptions
{
    public class CoreException : Exception
    {
        public CoreException()
        {
        }

        public CoreException(string message)
            : base(message)
        {
        }

        public CoreException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public CoreException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CoreException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}