using UserDataAccessLayer.InternalServices;
using Microsoft.EntityFrameworkCore;
using UserDataAccessLayer.Entities;
using UserDataAccessLayer.UserAppDbContext;

namespace UserDataAccessLayer.UserAppRepo
{
    public class UserRepo : IUserRepo
    {
        private readonly UserAppDbContextPostgre _context;
        private readonly IUserMapper _userMapper;
        private readonly AutoMapper.IMapper _mapper;
        public UserRepo(IUserMapper userMapper, UserAppDbContextPostgre context)
        {
           
            _context = context;
            _userMapper = userMapper;
            _mapper = _userMapper.InitializeMapper();
        }
        public async Task CreateUserAsync(InternalUser user)
        {
            await _context.internalUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var userToRemove = _context.internalUsers.FirstOrDefault(c => c.Id == id);
            if (userToRemove != null)
            {
                _context.internalUsers.Remove(userToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<InternalUser>> GetAllInternalUsersAsync()
        {
            return await _context.internalUsers.ToListAsync();
        }

        public async Task<IEnumerable<ReqResUser>> GetAllReqResUsersAsync()
        {
            var internalUsers = await _context.internalUsers.ToListAsync();
            return _mapper.Map<IEnumerable<ReqResUser>>(internalUsers);
        }

        public async Task<InternalUser> GetInternalUserAsync(Guid id)
        {
            return await _context.internalUsers.FirstOrDefaultAsync(m => m.Id == id);

        }

        public async Task<ReqResUser> GetReqResUserAsync(Guid id)
        {
            var internalUser = await _context.internalUsers.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<ReqResUser>(internalUser);
        }

        public bool InternalUserExistsAsync(Guid id)
        {
            return _context.internalUsers.Any(m => m.Id == id);
        }

        public async Task UpdateUserAsync(InternalUser user)
        {
            var existingUser = await _context.internalUsers.FirstOrDefaultAsync(c => c.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.UserName = user.UserName;
                existingUser.Role = user.Role;

               
                await _context.SaveChangesAsync();
            }
        }

    }
}
