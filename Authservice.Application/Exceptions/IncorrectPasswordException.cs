using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authservice.Application.Exceptions
{
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException() : base("Incorrect Password given")
        {

        }

        public IncorrectPasswordException(Exception innerException) : base("Incorrect Password given", innerException)
        {
        }
    }
}
