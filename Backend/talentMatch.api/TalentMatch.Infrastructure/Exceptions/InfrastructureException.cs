using System.Globalization;
using System.Runtime.Serialization;

namespace TalentMatch.Infrastructure.Exceptions
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException()
        {
        }

        public InfrastructureException(string message)
            : base(message)
        {
        }

        public InfrastructureException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public InfrastructureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected InfrastructureException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}