using AutoMapper;
using ManagerUsersGroups.Repository.Entities;
using ManagerUsersGroups.Repository.Exceptions;
using ManagerUsersGroups.Repository.Interfaces;
using ManagerUsersGroups.Repository.LDAP.Extensions;
using ManagerUsersGroups.Repository.LDAP.Options;
using Microsoft.Extensions.Options;
using System.DirectoryServices.Protocols;

namespace ManagerUsersGroups.Repository.LDAP.Implementations
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DirectoryConnection _directoryConnection;
        private readonly IMapper _mapper;
        private readonly IOptionsSnapshot<LDAPOptions> _options;

        public GroupRepository(IMapper mapper, DirectoryConnection directoryConnection, IOptionsSnapshot<LDAPOptions> options)
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

        public Task<bool> EntityInGroup(string groupSid, string entitySid, CancellationToken cancellationToken = default)
        {
            try
            {
                SearchResultEntry searchResultGroup = GetSearchResultEntry($"(&(objectClass=group)(objectCategory=group)(objectSid={groupSid}))").FirstOrDefault() 
                    ?? throw new RepositoryNotExistEntityException(groupSid, "Group with this sid not exist.");
                
                SearchResultEntry searchResultEntity = GetSearchResultEntry($"(&(|(&(objectClass=group)(objectCategory=group))(&(objectClass=user)(objectCategory=person)))(objectSid={groupSid}))").FirstOrDefault() 
                    ?? throw new RepositoryNotExistEntityException(entitySid, "Entity with this sid not exist.");

                return Task.FromResult(searchResultGroup.GetPropArray("memberOf").Contains(searchResultEntity.GetProp("distinguishedName")));
            }
            catch (RepositoryException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unknown error while check entity in group.", e);
            }
        }

        public Task<IEnumerable<GroupEntity>> Find(string findStr, CancellationToken cancellationToken = default)
        {
            try
            {
                return Task.FromResult(_mapper.Map<IEnumerable<SearchResultEntry>, IEnumerable<GroupEntity>>(GetSearchResultEntry($"(&(objectClass=group)(objectCategory=group)(anr={findStr}))")));
            }
            catch (RepositoryException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unknown error while searching group.", e);
            }
        }

        public Task<GroupEntity> GetBySID(string sid, CancellationToken cancellationToken = default)
        {
            try
            {
                return Task.FromResult(_mapper.Map<SearchResultEntry, GroupEntity>(GetSearchResultEntry($"(&(objectClass=group)(objectCategory=group)(objectSid={sid}))").FirstOrDefault()));
            }
            catch (RepositoryException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unknown error while get group.", e);
            }
        }

        public Task<GroupEntity> GetByDistinguishedName(string distinguishedName, CancellationToken cancellationToken = default)
        {
            try
            {
                return Task.FromResult(_mapper.Map<SearchResultEntry, GroupEntity>(GetSearchResultEntry($"(&(objectClass=group)(objectCategory=group)(distinguishedName={distinguishedName}))").FirstOrDefault()));
            }
            catch (RepositoryException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unknown error while get group.", e);
            }
        }

        public Task<IEnumerable<GroupEntity>> GetUserGroups(string userSid, CancellationToken cancellationToken = default)
        {
            try
            {
                SearchResultEntry searchResult = GetSearchResultEntry($"(&(objectClass=user)(objectCategory=person)(objectSid={userSid}))").FirstOrDefault() ?? throw new RepositoryNotExistEntityException(userSid, "User with this sid not exist.");

                return Task.FromResult(_mapper.Map<IEnumerable<SearchResultEntry>, IEnumerable<GroupEntity>>(GetSearchResultEntry($"(&(objectClass=group)(objectCategory=group)(member={searchResult.GetProp("distinguishedName")}))")));
            }
            catch (RepositoryException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Unknown error while get groups.", e);
            }
        }
    }
}
