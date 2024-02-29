using AuthenticationDataAccessLayer.AuthentcationAppDbContext;
using AuthenticationDataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationDataAccessLayer.AuthenticationRepo
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        private readonly AuthenticationDbContextPostgre context;

        public AuthenticationRepo (AuthenticationDbContextPostgre context)
        {
            this.context = context;
        }

        public async Task<User> GetUserAsync(string uName)
        {
            if (context == null) 
            {
                throw new ArgumentNullException(nameof(context));
            }
            var user =await context.users.FirstOrDefaultAsync(x => x.UserName == uName);
            return user ?? new User(); // returning decault user if user returns null
        }
    }
}
