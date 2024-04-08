using ManagerUsersGroups.Repository.Entities;

namespace ManagerUsersGroups.Repository.Interfaces
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<IEnumerable<UserEntity>> GetGroupUsers(string groupSid, CancellationToken cancellationToken = default);
    }
}
