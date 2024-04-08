using ManagerUsersGroups.Repository.Entities;
using ManagerUsersGroups.Repository.Interfaces;

namespace ManagerUsersGroups.Repository.AD.Implementations
{
    public class GroupRepository : IGroupRepository
    {


        public GroupRepository() { }

        public Task<bool> EntityInGroup(string groupSid, string entitySid, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupEntity>> Find(string findStr, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<GroupEntity> GetBySID(string sid, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupEntity>> GetUserGroups(string userSid, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
