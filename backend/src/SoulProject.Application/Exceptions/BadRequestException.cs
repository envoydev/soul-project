using FluentValidation.Results;

namespace SoulProject.Application.Exceptions;

public class BadRequestException : Exception
{
    public IDictionary<string, string[]> Errors { get; }
    
    public BadRequestException(string message) : base(message)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public BadRequestException(string message, IEnumerable<ValidationFailure> failures) : this(message)
    {
        Errors = failures
                 .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                 .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}