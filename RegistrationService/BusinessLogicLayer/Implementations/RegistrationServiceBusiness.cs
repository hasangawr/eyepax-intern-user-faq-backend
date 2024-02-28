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
        public void AddUser(AddUserReq addUserReq)
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
            userRepo.CreateUser(user);
        }

    }
}
