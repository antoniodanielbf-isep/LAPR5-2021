using System;

namespace DDDSample1.Domain.Shared
{
    public class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException(string message) : base(message)
        {
        }

        public BusinessRuleValidationException(string message, string details) : base(message)
        {
            Details = details;
        }

        public string Details { get; }
    }
}