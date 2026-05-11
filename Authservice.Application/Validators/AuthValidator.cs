using Authservice.Application.DTO;
using Authservice.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authservice.Application.Validators
{
    public class AuthValidator
    {
        public static void ValidateLoginDTO(LoginDTO objdto)
        {
            var errors = new List<ValidationError>();

            if (string.IsNullOrWhiteSpace(objdto.emailId))
            {
                errors.Add(new ValidationError("emailId", "Email is required"));
            }

            if (string.IsNullOrWhiteSpace(objdto.password))
            {
                errors.Add(new ValidationError("password", "Password is required"));
            }

            else if(objdto.password.Length < 6)
            {
                errors.Add(new ValidationError("password", "Password must be at least 6 characters"));
            }

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }

        public static void ValidateRegisterDTO(RegisterUserDTO registerDTO)
        {
            var errors = new List<ValidationError>();

            if (string.IsNullOrWhiteSpace(registerDTO.name))
            {
                errors.Add(new ValidationError("name", "Name is required"));
            }
            else if (registerDTO.name.Length < 3)
            {
                errors.Add(new ValidationError("name", "Name must be at least 3 characters"));
            }


            //email validation
            if (string.IsNullOrWhiteSpace(registerDTO.email))
            {
                errors.Add(new ValidationError("email", "Email is required"));
            }
            else if (!IsValidEmail(registerDTO.email))
            {
                errors.Add(new ValidationError("email", "Email format is invalid"));
            }

            // Password validation
            if (string.IsNullOrWhiteSpace(registerDTO.password))
            {
                errors.Add(new ValidationError("password", "Password is required"));
            }
            else if (registerDTO.password.Length < 8)
            {
                errors.Add(new ValidationError("password", "Password must be at least 8 characters"));
            }
            else if (!ContainsUpperCase(registerDTO.password))
            {
                errors.Add(new ValidationError("password", "Password must contain at least one uppercase letter"));
            }
            else if (!ContainsSpecialCharacter(registerDTO.password))
            {
                errors.Add(new ValidationError("password", "Password must contain at least one special character"));
            }

            // Role validation
            if (registerDTO.roleId == null || (registerDTO.roleId != 1 && registerDTO.roleId != 2))
            {
                errors.Add(new ValidationError("roleId", "Invalid role ID"));
            }

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }
        }


        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static bool ContainsUpperCase(string password)
        {
            return password.Any(char.IsUpper);
        }
        private static bool ContainsSpecialCharacter(string password)
        {
            return password.Any(c=>!char.IsLetterOrDigit(c));   
        }
    }
}
