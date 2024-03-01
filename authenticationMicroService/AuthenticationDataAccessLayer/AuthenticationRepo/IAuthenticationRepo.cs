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
    }
}
