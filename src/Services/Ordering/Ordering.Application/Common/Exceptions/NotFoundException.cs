using System.Runtime.Serialization;

namespace Ordering.Application.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() : base()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(string name, object key) : base($"Entity \"{name}\" was not found.")
        {
        }
    }
}
