using FluentValidation.Results;

namespace Ordering.Application.Common.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }
        public ValidationException() : 
            base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failurGroup => failurGroup.Key, failurGroup => failurGroup.ToArray());
        } 
    }
}
