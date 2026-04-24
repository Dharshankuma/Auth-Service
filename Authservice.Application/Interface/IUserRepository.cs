using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authservice.Application.Interface
{
    public interface IUserRepository
    {
        Task<User?> DoGetEmailByAsync(string email);
        Task DoAddUser(User user);
    }
}
