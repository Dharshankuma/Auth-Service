using Authservice.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authservice.Application.Interface
{
    public interface IAuthServices
    {
        Task<string> DoRegisterUser(RegisterUserDTO objdto);
    }
}
