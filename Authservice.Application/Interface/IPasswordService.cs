using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authservice.Application.Interface
{
    public interface IPasswordService
    {
        Task<string> HashPassword(string password);

        Task<bool> VerifyPassword(string password, string hashedPassword);
    }
}
