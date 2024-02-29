using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataAccessLayer.Entities;

namespace UserDataAccessLayer.UserAppRepo
{
    public interface IUserRepo
    {
        Task<IEnumerable<InternalUser>> GetAllInternalUsersAsync();
        Task<IEnumerable<ReqResUser>> GetAllReqResUsersAsync();
        Task<ReqResUser> GetReqResUserAsync(Guid id);
        Task<InternalUser> GetInternalUserAsync(Guid id);
        Task UpdateUserAsync(InternalUser user);
        Task DeleteUserAsync(Guid id);
        Task CreateUserAsync(InternalUser user);
        bool InternalUserExistsAsync(Guid id);
    }
}
