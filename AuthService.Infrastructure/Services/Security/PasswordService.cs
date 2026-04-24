using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authservice.Application.Interface;

namespace AuthService.Infrastructure.Services.Security
{
    public class PasswordService : IPasswordService
    {
        public async Task<string> HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
