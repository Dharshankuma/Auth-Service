using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public  class User
    {
        public int Id { get; set; }
        public string Identifier { get;  set; } = Guid.NewGuid().ToString();
        public string Password { get; set; } = null;
        public string UserName { get; set; } = null;
        public string Email { get; set; } = null;
        public int RoleId { get; set; } = 0;
        public string RoleName { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
