using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authservice.Application.DTO
{
    public class RegisterUserDTO
    {
        public string? name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public int? roleId { get; set; }

    }
}
