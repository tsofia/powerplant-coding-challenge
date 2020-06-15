using System;

namespace Domain.Services.Exceptions
{
    public class InvalidParametersException : Exception
    {
        public InvalidParametersException() : base("The parameters you have provided are invalid.")
        {
        }

        public InvalidParametersException(string message) : base(message)
        {
        }

        public InvalidParametersException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}