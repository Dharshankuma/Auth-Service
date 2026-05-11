using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authservice.Application.Exceptions
{
    public class LoginAlreadyTakenException : Exception
    {
        public LoginAlreadyTakenException() : base("Login already taken")
        { }

        public LoginAlreadyTakenException(Exception innerException) : base("Login already Taken", innerException)
        { }
    }
}