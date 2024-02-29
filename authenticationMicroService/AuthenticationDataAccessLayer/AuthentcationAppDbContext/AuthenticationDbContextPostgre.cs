using AuthenticationDataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace AuthenticationDataAccessLayer.AuthentcationAppDbContext
{
    public class AuthenticationDbContextPostgre :DbContext
    {
        public AuthenticationDbContextPostgre(DbContextOptions<AuthenticationDbContextPostgre> options) : base(options) { }
        public DbSet<User> users { get; set;}
    }
}
