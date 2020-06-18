using System;

namespace Domain.Services.Exceptions
{
    public class InsufficientPowerException : Exception
    {
        public InsufficientPowerException() 
            : base("The power plants in the provided production might not fulfill the needed load. Please contact the dispatch team right away!")
        {
        }
    }
}