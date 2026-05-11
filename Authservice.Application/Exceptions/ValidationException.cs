using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authservice.Application.Exceptions
{
    public class ValidationException : Exception
    {

        public ValidationException(IReadOnlyCollection<ValidationError> errors) : base("Validation failed")
        {
            Errors = errors;
        }


        public IReadOnlyCollection<ValidationError> Errors { get; }
    }

    public record ValidationError(string PropertyName, string ErrorMessage);
}
