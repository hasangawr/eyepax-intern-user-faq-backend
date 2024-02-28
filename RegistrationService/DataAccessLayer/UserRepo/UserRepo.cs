using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UserRepo
{
    public class UserRepo: IUserRepo
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async  Task<User> CreateUser(User user)
        {
            try
            {
                
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                return user;

            }
            catch (Exception ex)
            {
                //Console.Write(ex.Message);
                throw new Exception(ex .ToString());
            }
            
            
        }

        public async Task<bool> IsUsernameUnique(string InputUsername)
        {
            
            bool result = await dbContext.Users.AnyAsync(u => u.UserName == InputUsername);
            return !result;
        }
    }
}
