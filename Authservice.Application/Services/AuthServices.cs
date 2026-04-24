using Authservice.Application.DTO;
using Authservice.Application.Interface;
using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authservice.Application.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordService _pass;
        private readonly IDateTimeProvider _date;

        public AuthServices(IUserRepository repo, IPasswordService pass,IDateTimeProvider date)
        {
            _repo = repo;
            _pass = pass;
            _date = date;
        }

        public async Task<string> DoRegisterUser(RegisterUserDTO objdto)
        {
            var existing = await _repo.DoGetEmailByAsync(objdto.email);
            if(existing != null)
            {
                throw new Exception("Email already exists");
            }

            var hashedPassword =  await _pass.HashPassword(objdto.password);
            var roleName = objdto.roleId == 1 ? "Admin" : "User";

            var user = new User
            {
                UserName = objdto.name,
                Password = hashedPassword,
                Email = objdto.email,
                RoleId = (int)objdto.roleId,
                RoleName = roleName,
                CreatedAt = _date.UtcNow
            };

            await _repo.DoAddUser(user);

            return "User Registerd Successfully";
        }
    }
}
