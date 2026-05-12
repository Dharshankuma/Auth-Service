using Authservice.Application.DTO;
using Authservice.Application.Interface;
using Authservice.Application.Validators;
using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Authservice.Application.Exceptions;

namespace Authservice.Application.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordService _pass;
        private readonly IDateTimeProvider _date;
        private readonly ITokenProvider _token;
        private readonly ILogger<AuthServices> _logger;

        public AuthServices(IUserRepository repo, IPasswordService pass, IDateTimeProvider date, ITokenProvider token, ILogger<AuthServices> logger)
        {
            _repo = repo;
            _pass = pass;
            _date = date;
            _token = token;
            _logger = logger;
        }

        public async Task<string> DoRegisterUser(RegisterUserDTO objdto)
        {

            _logger.LogInformation("Registering user with email: {Email}", objdto.email);
            //validate input
            AuthValidator.ValidateRegisterDTO(objdto);

            var existing = await _repo.DoGetEmailByAsync(objdto.email);
            if(existing != null)
            {
                throw new LoginAlreadyTakenException();
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

            _logger.LogInformation("User registered successfully. Email: {Email}, UserId: {UserId}", objdto.email, user.Identifier);

            return "User Registerd Successfully";
        }

        public async Task<string> Login(LoginDTO objdto)
        { 
            _logger.LogInformation("Login attempt for email: {Email}", objdto.emailId);
            //validate input
            AuthValidator.ValidateLoginDTO(objdto);
            var user = await _repo.DoGetEmailByAsync(objdto.emailId);
            if(user == null)
            {
                throw new UserAccountNotFoundOrGivenPasswordIsIncorrect(objdto.emailId);
            }
            var isPasswordValid = await _pass.VerifyPassword(objdto.password, user.Password);
            if (!isPasswordValid)
            {
                throw new IncorrectPasswordException();
            }

            var token = await _token.GenerateToken(user);

            _logger.LogInformation("Login successful for email: {Email}, UserId: {UserId}", objdto.emailId, user.Identifier);
            return token;
        }
    }
}
