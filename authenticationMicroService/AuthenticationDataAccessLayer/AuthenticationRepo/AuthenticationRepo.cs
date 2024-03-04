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

        public async Task SaveUser(Guid Id, string UserName, string Password)
        {
            var user = new User{
                Id = Id,
                UserName = UserName,
                Password = Password
            };
            await context.users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(string uName)
        {
            if (context == null) 
            {
                throw new ArgumentNullException(nameof(context));
            }
            var user =await context.users.FirstOrDefaultAsync(x => x.UserName == uName);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var userToRemove = context.users.FirstOrDefault(c => c.Id == id);
            if (userToRemove != null)
            {
                context.users.Remove(userToRemove);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(Guid id, string name, string password)
        {
            var existingUser = await context.users.FirstOrDefaultAsync(c => c.Id == id);
            if (existingUser != null)
            {
                existingUser.UserName = name;
                existingUser.Password = password;
                await context.SaveChangesAsync();
            }
        }
    }
}
