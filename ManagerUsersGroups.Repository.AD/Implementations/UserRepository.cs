using AutoMapper;
using ManagerUsersGroups.Repository.AD.Extensions;
using ManagerUsersGroups.Repository.AD.Options;
using ManagerUsersGroups.Repository.Entities;
using ManagerUsersGroups.Repository.Exceptions;
using ManagerUsersGroups.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.DirectoryServices;
using System.Security.Cryptography;

namespace ManagerUsersGroups.Repository.AD.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DirectoryEntry _directoryEntry;
        private readonly IMapper _mapper;


        public UserRepository(IOptionsSnapshot<ADOptions> options, IMapper mapper) 
        {
            _directoryEntry = options.Value.LoginType switch
            {
                LoginType.UserContext => new DirectoryEntry(),
                LoginType.UserContextWithPath => new DirectoryEntry(options.Value.Path),
                LoginType.PathWithLoginAndPass => new DirectoryEntry(options.Value.Path, options.Value.UserName, options.Value.Password),
                LoginType.PathWithLoginAndPassAndAuthenticationType => new DirectoryEntry(options.Value.Path, options.Value.UserName, options.Value.Password, options.Value.AuthenticationTypes),
                _ => throw new ArgumentException("Invalid LogingType.", nameof(options)),
            };

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<UserEntity>> Find(string findStr, CancellationToken cancellationToken = default)
        {
            try
            {
                DirectorySearcher directorySearcher = new DirectorySearcher(_directoryEntry, $"(&(objectClass=user)(objectCategory=person)(anr={findStr}))");

                return Task.FromResult(_mapper.Map<IEnumerable<SearchResult>, IEnumerable<UserEntity>>(directorySearcher.FindAll().Cast<SearchResult>()));
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
                DirectorySearcher directorySearcher = new DirectorySearcher(_directoryEntry, $"(&(objectClass=user)(objectCategory=person)(objectSid={sid}))");

                return Task.FromResult(_mapper.Map<SearchResult, UserEntity>(directorySearcher.FindOne()));
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
                DirectorySearcher directorySearcher = new DirectorySearcher(_directoryEntry, $"(&(objectClass=user)(objectCategory=person)(distinguishedName={distinguishedName}))");

                return Task.FromResult(_mapper.Map<SearchResult, UserEntity>(directorySearcher.FindOne()));
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
                DirectorySearcher directorySearcherGroup = new DirectorySearcher(_directoryEntry, $"(&(objectClass=group)(objectCategory=group)(objectSid={groupSid}))");

                SearchResult searchResult = directorySearcherGroup.FindOne() ?? throw new RepositoryNotExistEntityException(groupSid, "Group with this sid not exist.");

                DirectorySearcher directorySearcherUsers = new DirectorySearcher(_directoryEntry, $"(&(objectClass=user)(objectCategory=person)(memberOf={searchResult.GetProp("distinguishedName")}))");

                return Task.FromResult(_mapper.Map<IEnumerable<SearchResult>, IEnumerable<UserEntity>>(directorySearcherUsers.FindAll().Cast<SearchResult>()));
            }
            catch(RepositoryException)
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
