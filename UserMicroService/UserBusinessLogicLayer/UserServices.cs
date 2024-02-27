using UserDataAccessLayer.Entities;
using UserDataAccessLayer.UserAppRepo;

namespace UserBusinessLogicLayer
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepo _userRepo;
        public UserServices(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public void CreateUserAsync(PostUser postUser)
        {
            var user = new InternalUser()
            {
                Id = Guid.NewGuid(),
                FirstName = postUser.FirstName,
                LastName = postUser.LastName,
                Password = postUser.Password,
                Email = postUser.Email,
                UserName = postUser.UserName
            };
            _userRepo.CreateUserAsync(user);
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
                LastName = postUser.FirstName,
                Password = postUser.Password,
                Email = postUser.Email,
                UserName = postUser.UserName,
                Role = postUser.Role
            };
            await _userRepo.UpdateUserAsync(user);
        }
    }
}
