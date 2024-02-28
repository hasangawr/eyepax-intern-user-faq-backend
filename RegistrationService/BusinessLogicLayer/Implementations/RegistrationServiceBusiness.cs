using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementations
{
    public class RegistrationServiceBusiness : IRegistrationService

    {
        private readonly IUserRepo userRepo;
        private readonly IPasswordHasher passwordHasher;
        public RegistrationServiceBusiness(IUserRepo userRepo, IPasswordHasher passwordHasher)
        {
            this.userRepo = userRepo;
            this.passwordHasher = passwordHasher;

        }
        public async Task<User> AddUser(AddUserReq addUserReq)
        {
            string username = addUserReq.UserName;
            bool isUsernameUnique = await userRepo.IsUsernameUnique(username);
            Console.WriteLine(isUsernameUnique);
            if (isUsernameUnique)
            {
                var passwordHash = passwordHasher.Hash(addUserReq.Password);
                User user = new User()
                {
                    Id = new Guid(),
                    FirstName = addUserReq.FirstName,
                    LastName = addUserReq.LastName,
                    Email = addUserReq.Email,
                    Password = passwordHash,
                    UserName = addUserReq.UserName,
                    Role = addUserReq.Role
                };
                User createdUser = await userRepo.CreateUser(user);
                return createdUser;

            }
            else
            {
                //here's an issue with the adding a user with a non-unique username
                throw new InvalidOperationException("Username already exists!");
            }

        }

    }
}
