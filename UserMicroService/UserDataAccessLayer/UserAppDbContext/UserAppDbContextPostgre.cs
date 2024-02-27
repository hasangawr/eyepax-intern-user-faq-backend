using Microsoft.EntityFrameworkCore;
using UserDataAccessLayer.Entities;

namespace UserDataAccessLayer.UserAppDbContext
{
    public class UserAppDbContextPostgre :DbContext
    {
        public UserAppDbContextPostgre(DbContextOptions<UserAppDbContextPostgre> options) :base(options) { }

        public DbSet<InternalUser> internalUsers { get; set; }
    }
}
