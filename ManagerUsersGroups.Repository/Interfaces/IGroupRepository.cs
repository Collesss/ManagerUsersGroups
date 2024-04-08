using ManagerUsersGroups.Repository.Entities;

namespace ManagerUsersGroups.Repository.Interfaces
{
    public interface IGroupRepository : IRepository<GroupEntity>
    {
        Task<IEnumerable<GroupEntity>> GetUserGroups(string userSid, CancellationToken cancellationToken = default);

        Task<bool> EntityInGroup(string groupSid, string entitySid, CancellationToken cancellationToken = default);
    }
}
