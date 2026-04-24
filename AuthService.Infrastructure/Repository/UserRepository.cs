using Authservice.Application.Interface;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDBContext _db;

        public UserRepository(AuthDBContext db)
        {
            _db = db;
        }   
        public async Task<User> DoGetEmailByAsync(string email)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task DoAddUser(User user)
        {
            _db.User.Add(user);
            await _db.SaveChangesAsync();
        }


    }
}
