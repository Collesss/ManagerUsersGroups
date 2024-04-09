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

        public Task<IEnumerable<UserEntity>> Find(string findStr, CancellationToken cancellationToken = default)
        {
            try
            {
                DirectoryRequest request = new SearchRequest("", $"(&(objectClass=user)(objectCategory=person)(anr={findStr}))", SearchScope.Subtree);

                SearchResponse response = (SearchResponse)_directoryConnection.SendRequest(request);

                if (response.ResultCode != ResultCode.Success)
                    throw new RepositoryException($"{response.ResultCode}. {response.ErrorMessage}");

                return Task.FromResult(_mapper.Map<IEnumerable<SearchResultEntry>, IEnumerable<UserEntity>>(response.Entries.Cast<SearchResultEntry>()));
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
                
                DirectoryRequest request = new SearchRequest(_options.Value.SearchRoot, $"(&(objectClass=user)(objectCategory=person)(objectSid={sid}))", SearchScope.Subtree);
                
                SearchResponse response = (SearchResponse)_directoryConnection.SendRequest(request);

                if (response.ResultCode != ResultCode.Success)
                    throw new RepositoryException($"{response.ResultCode}. {response.ErrorMessage}");

                return Task.FromResult(_mapper.Map<SearchResultEntry, UserEntity>(response.Entries.Cast<SearchResultEntry>().FirstOrDefault()));
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
                //DirectorySearcher directorySearcher = new DirectorySearcher(_directoryEntry, $"(&(objectClass=user)(objectCategory=person)(distinguishedName={distinguishedName}))");

                //return Task.FromResult(_mapper.Map<SearchResult, UserEntity>(directorySearcher.FindOne()));

                throw new Exception("error");
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
                //DirectorySearcher directorySearcherGroup = new DirectorySearcher(_directoryEntry, $"(&(objectClass=group)(objectCategory=group)(objectSid={groupSid}))");

                //SearchResult searchResult = directorySearcherGroup.FindOne() ?? throw new RepositoryNotExistEntityException(groupSid, "Group with this sid not exist.");

                //DirectorySearcher directorySearcherUsers = new DirectorySearcher(_directoryEntry, $"(&(objectClass=user)(objectCategory=person)(memberOf={searchResult.GetProp("distinguishedName")}))");

                //return Task.FromResult(_mapper.Map<IEnumerable<SearchResult>, IEnumerable<UserEntity>>(directorySearcherUsers.FindAll().Cast<SearchResult>()));

                throw new Exception("error");
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
