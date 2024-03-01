
using UserBusinessLogicLayer.PasswordServices;
using UserBusinessLogicLayer.RabbitServices;
using UserDataAccessLayer.Entities;
using UserDataAccessLayer.UserAppRepo;

namespace UserBusinessLogicLayer
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepo _userRepo;
        private readonly IPasswordHasher _pwServices;
        
        private readonly IMessageBusClient _messageClient;


        public UserServices(IUserRepo userRepo, IPasswordHasher pwServices, IMessageBusClient messageClient )
        {
            _userRepo = userRepo;
            _pwServices = pwServices;
           
            _messageClient = messageClient;
            
        }
        public async Task<InternalUser> CreateUserAsync(PostUser postUser)
        {

            string username = postUser.UserName;
            bool isUsernameUnique = await _userRepo.IsUsernameUnique(username);
            
            if (isUsernameUnique)
            {
                string passwordHash = _pwServices.Hash(postUser.Password);
                InternalUser user = new InternalUser()
                {
                    Id = Guid.NewGuid(),
                    FirstName = postUser.FirstName,
                    LastName = postUser.LastName,
                    Password = passwordHash,
                    Email = postUser.Email,
                    UserName = postUser.UserName,
                    Role = postUser.Role
                };
                InternalUser createdUser = await _userRepo.CreateUserAsync(user);

                //synchronizing authentication Db
                var pubUser = new UserMessage() { Id = user.Id, UserName = user.UserName, Password = user.Password };
                _messageClient.PublishNewUser(pubUser);
             


                return createdUser;

            }
            else
            {
                //here's an issue with the adding a user with a non-unique username
                throw new InvalidOperationException("Username already exists!");
            }

        }

        public void DeleteUserAsync(Guid id)
        {
            _userRepo.DeleteUserAsync(id);
        }

        public async Task<IEnumerable<InternalUser>> GetAllInternalUsersAsync()
        {
            return await _userRepo.GetAllInternalUsersAsync();
        }

        public async Task<IEnumerable<ReqResUser>> GetAllReqResUsersAsync()
        {
            return await _userRepo.GetAllReqResUsersAsync();
        }

        public async Task<InternalUser> GetInternalUserAsync(Guid id)
        {
            return await _userRepo.GetInternalUserAsync(id);
        }

        public async Task<ReqResUser> GetReqResUserAsync(Guid id)
        {
            return await _userRepo.GetReqResUserAsync(id);
        }

        public async Task UpdateUserAsync(Guid id, PostUser postUser)
        {
            var user = new InternalUser()
            {
                Id = id,
                FirstName = postUser.FirstName,
                LastName = postUser.LastName,
                Password = _pwServices.Hash(postUser.Password),
                Email = postUser.Email,
                UserName = postUser.UserName,
                Role = postUser.Role
            };
            await _userRepo.UpdateUserAsync(user);
        }
    }
}
