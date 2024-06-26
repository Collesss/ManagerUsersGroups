﻿using AutoMapper;
using ManagerUsersGroups.Repository.AD.Extensions;
using ManagerUsersGroups.Repository.AD.Options;
using ManagerUsersGroups.Repository.Entities;
using ManagerUsersGroups.Repository.Exceptions;
using ManagerUsersGroups.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.DirectoryServices;

namespace ManagerUsersGroups.Repository.AD.Implementations
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DirectoryEntry _directoryEntry;
        private readonly IMapper _mapper;

        public GroupRepository(IOptionsSnapshot<ADOptions> options, IMapper mapper)
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

        public Task<bool> EntityInGroup(string groupSid, string entitySid, CancellationToken cancellationToken = default)
        {
            try
            {
                DirectorySearcher directorySearcherGroup = new DirectorySearcher(_directoryEntry, $"(&(objectClass=group)(objectCategory=group)(objectSid={groupSid}))");
                DirectorySearcher directorySearcherEntity = new DirectorySearcher(_directoryEntry, $"(&(|(&(objectClass=group)(objectCategory=group))(&(objectClass=user)(objectCategory=person)))(objectSid={groupSid}))");

                SearchResult searchResultGroup = directorySearcherGroup.FindOne() ?? throw new RepositoryNotExistEntityException(groupSid, "Group with this sid not exist.");
                SearchResult searchResultEntity = directorySearcherGroup.FindOne() ?? throw new RepositoryNotExistEntityException(entitySid, "Entity with this sid not exist.");

                return Task.FromResult(searchResultGroup.Properties["memberOf"].Cast<string>().Contains(searchResultEntity.GetProp("distinguishedName")));
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
                DirectorySearcher directorySearcher = new DirectorySearcher(_directoryEntry, $"(&(objectClass=group)(objectCategory=group)(anr={findStr}))");

                return Task.FromResult(_mapper.Map<IEnumerable<SearchResult>, IEnumerable<GroupEntity>>(directorySearcher.FindAll().Cast<SearchResult>()));
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
                DirectorySearcher directorySearcher = new DirectorySearcher(_directoryEntry, $"(&(objectClass=group)(objectCategory=group)(objectSid={sid}))");

                return Task.FromResult(_mapper.Map<SearchResult, GroupEntity>(directorySearcher.FindOne()));
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
                DirectorySearcher directorySearcher = new DirectorySearcher(_directoryEntry, $"(&(objectClass=group)(objectCategory=group)(distinguishedName={distinguishedName}))");

                return Task.FromResult(_mapper.Map<SearchResult, GroupEntity>(directorySearcher.FindOne()));
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
                DirectorySearcher directorySearcherUser = new DirectorySearcher(_directoryEntry, $"(&(objectClass=user)(objectCategory=person)(objectSid={userSid}))");

                SearchResult searchResult = directorySearcherUser.FindOne() ?? throw new RepositoryNotExistEntityException(userSid, "User with this sid not exist.");

                DirectorySearcher directorySearcherGroups = new DirectorySearcher(_directoryEntry, $"(&(objectClass=group)(objectCategory=group)(member={searchResult.GetProp("distinguishedName")}))");

                return Task.FromResult(_mapper.Map<IEnumerable<SearchResult>, IEnumerable<GroupEntity>>(directorySearcherGroups.FindAll().Cast<SearchResult>()));
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
