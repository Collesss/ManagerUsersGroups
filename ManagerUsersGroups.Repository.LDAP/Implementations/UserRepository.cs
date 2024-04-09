using AutoMapper;
using ManagerUsersGroups.Repository.Entities;
using ManagerUsersGroups.Repository.Exceptions;
using ManagerUsersGroups.Repository.Interfaces;
using ManagerUsersGroups.Repository.LDAP.Options;
using Microsoft.Extensions.Options;
using System.DirectoryServices.Protocols;

namespace ManagerUsersGroups.Repository.LDAP.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DirectoryConnection _directoryConnection;
        private readonly IMapper _mapper;
        private readonly IOptionsSnapshot<LDAPOptions> _options;

        public UserRepository(IMapper mapper, DirectoryConnection directoryConnection, IOptionsSnapshot<LDAPOptions> options)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _directoryConnection = directoryConnection ?? throw new ArgumentNullException(nameof(directoryConnection));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        private IEnumerable<SearchResultEntry> GetSearchResultEntry(string filter)
        {
            DirectoryRequest request = new SearchRequest(_options.Value.SearchRoot, filter, SearchScope.Subtree);
            SearchResponse response = (SearchResponse)_directoryConnection.SendRequest(request);

            if (response.ResultCode != ResultCode.Success)
                throw new RepositoryException($"{response.ResultCode}. {response.ErrorMessage}");

            return response.Entries.Cast<SearchResultEntry>();
        }


        public Task<IEnumerable<UserEntity>> Find(string findStr, CancellationToken cancellationToken = default)
        {
            try
            {
                return Task.FromResult(_mapper.Map<IEnumerable<SearchResultEntry>, IEnumerable<UserEntity>>(GetSearchResultEntry($"(&(objectClass=user)(objectCategory=person)(anr={findStr}))")));
            }
            catch (RepositoryException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unknown error while searching user.", e);
            }
        }

        public Task<UserEntity> GetBySID(string sid, CancellationToken cancellationToken = default)
        {
            try
            {
                return Task.FromResult(_mapper.Map<SearchResultEntry, UserEntity>(GetSearchResultEntry($"(&(objectClass=user)(objectCategory=person)(objectSid={sid}))").FirstOrDefault()));
            }
            catch (RepositoryException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unknown error while get user.", e);
            }
        }

        public Task<UserEntity> GetByDistinguishedName(string distinguishedName, CancellationToken cancellationToken = default)
        {
            try
            {
                return Task.FromResult(_mapper.Map<SearchResultEntry, UserEntity>(GetSearchResultEntry($"(&(objectClass=user)(objectCategory=person)(distinguishedName={distinguishedName}))").FirstOrDefault()));
            }
            catch (RepositoryException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unknown error while get user.", e);
            }
        }

        public Task<IEnumerable<UserEntity>> GetGroupUsers(string groupSid, CancellationToken cancellationToken = default)
        {
            try
            {
                SearchResultEntry searchResultGroup = GetSearchResultEntry($"(&(objectClass=group)(objectCategory=group)(objectSid={groupSid}))").FirstOrDefault() 
                    ?? throw new RepositoryNotExistEntityException(groupSid, "Group with this sid not exist.");

                return Task.FromResult(_mapper.Map<IEnumerable<SearchResultEntry>, IEnumerable<UserEntity>>(GetSearchResultEntry($"(&(objectClass=user)(objectCategory=person)(memberOf={searchResultGroup.DistinguishedName}))")));
            }
            catch (RepositoryException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unknown error while get users.", e);
            }
        }
    }
}
