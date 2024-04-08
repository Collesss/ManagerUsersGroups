using ManagerUsersGroups.Repository.Entities;

namespace ManagerUsersGroups.Repository.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetBySID(string sid, CancellationToken cancellationToken = default);

        Task<T> GetByDistinguishedName(string distinguishedName, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> Find(string findStr, CancellationToken cancellationToken = default);
    }
}
