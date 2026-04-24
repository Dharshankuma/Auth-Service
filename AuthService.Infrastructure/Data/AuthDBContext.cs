using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthService.Domain.Entities;


namespace AuthService.Infrastructure.Data
{
    public class AuthDBContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options)
        {
        }
    }
}
