using System;

namespace TodoApi.Domain.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ErrorType ErrorType { get; init; }

        public ValidationException(string message, ErrorType errorType = ErrorType.ValidationError) : base(message)
        {
            ErrorType = errorType;
        }
    }
}
