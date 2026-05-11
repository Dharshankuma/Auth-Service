using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authservice.Application.Exceptions
{
    public class UserAccountNotFoundOrGivenPasswordIsIncorrect : Exception
    {
        public UserAccountNotFoundOrGivenPasswordIsIncorrect(string login) : base($"User account with ${login} is not found or given password is incorrect")
        {

        }

    }
}
