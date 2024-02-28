using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UserRepo
{
    public interface IUserRepo
    {
        Task CreateUser(User user); // create user
        
    }
}
