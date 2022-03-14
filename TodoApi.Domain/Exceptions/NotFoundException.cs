using System;

namespace TodoApi.Domain.Exceptions
{
    [Serializable]
    public class NotFoundException : ValidationException
    {
        public NotFoundException(string message) : base(message, ErrorType.NotFoundError)
        {
        }
    }
}
