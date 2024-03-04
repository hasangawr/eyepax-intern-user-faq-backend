using AuthenticationDataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationDataAccessLayer.AuthenticationRepo
{
    public interface IAuthenticationRepo
    {
        public Task<Entities.User> GetUserAsync(string uName);
        Task SaveUser(Guid Id, string UserName, string Password);
        Task DeleteUserAsync(Guid id);
        Task UpdateUserAsync(Guid id, string name, string password);
    }
}
