using AutoMapper;
using ManagerUsersGroups.Repository.LDAP.AutoMapperProfiles;
using ManagerUsersGroups.Repository.LDAP.Implementations;
using ManagerUsersGroups.Repository.Entities;
using ManagerUsersGroups.Repository.Interfaces;
using System.DirectoryServices.Protocols;
using Microsoft.Extensions.Options;
using ManagerUsersGroups.Repository.LDAP.Options;
using Moq;

namespace ManagerUsersGroups.Tests
{
    [TestClass]
    public class RepositoryLDAPUnitTest
    {
        [TestMethod]
        public void TestGetBySid()
        {
            #region arrange
            IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();

            LDAPOptions options = new LDAPOptions 
            {
                SearchRoot = "DC=office,DC=crocusgroup,DC=ru"
            };

            Mock<IOptionsSnapshot<LDAPOptions>> mockOptions = new Mock<IOptionsSnapshot<LDAPOptions>>();
            mockOptions
            .Setup(opts => opts.Value)
                .Returns(options);

            DirectoryConnection connection = new LdapConnection("office.crocusgroup.ru");

            IUserRepository userRepository = new UserRepository(mapper, connection, mockOptions.Object);
            
            string sid = "S-1-5-21-2851501073-1893086065-4185065217-4341";
            #endregion

            #region act
            #endregion

            UserEntity user = userRepository.GetBySID(sid).Result;

            #region assert
            Assert.AreEqual(sid, user.SID);
            #endregion
        }
    }
}
