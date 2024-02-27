using UserDataAccessLayer.Entities;

namespace UserBusinessLogicLayer
{
    public interface IUserServices
    {
        Task<IEnumerable<InternalUser>> GetAllInternalUsersAsync();
        Task<IEnumerable<ReqResUser>> GetAllReqResUsersAsync();
        Task<ReqResUser> GetReqResUserAsync(Guid id);
        Task<InternalUser> GetInternalUserAsync(Guid id);
        Task UpdateUserAsync(Guid id, PostUser postUser);
        void DeleteUserAsync(Guid id);
        void CreateUserAsync(PostUser postUser);
    }
}
