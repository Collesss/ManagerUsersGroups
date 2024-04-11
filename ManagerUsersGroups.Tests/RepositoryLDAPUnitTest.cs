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

            LDAPOptions options = new LDAPOptions { SearchRoot = string.Empty };
            Mock<IOptionsSnapshot<LDAPOptions>> mockOptions = new Mock<IOptionsSnapshot<LDAPOptions>>();
            mockOptions
            .Setup(opts => opts.Value)
                .Returns(options);


            Mock<SearchResultAttributeCollection> mockSearchResultAttributeCollection = new Mock<SearchResultAttributeCollection>();
            mockSearchResultAttributeCollection
                .SetupGet(mock => mock["objectSid"])
                    .Returns(new DirectoryAttribute("objectSid", new byte[] { 0x01, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x15, 0x00, 0x00, 0x00, 0x11, 0x74, 0xF6, 0xA9, 0x71, 0x33, 0xD6, 0x70, 0x01, 0x07, 0x73, 0xF9, 0xF5, 0x10, 0x00, 0x00 }));
            mockSearchResultAttributeCollection
                .Setup(mock => mock["sAMAccountName"])
                    .Returns(new DirectoryAttribute("sAMAccountName", new byte[] { 0x01, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x15, 0x00, 0x00, 0x00, 0x11, 0x74, 0xF6, 0xA9, 0x71, 0x33, 0xD6, 0x70, 0x01, 0x07, 0x73, 0xF9, 0xF5, 0x10, 0x00, 0x00 }));
            mockSearchResultAttributeCollection
                .Setup(mock => mock["displayName"])
                    .Returns(new DirectoryAttribute("displayName", new byte[] { 0x01, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x15, 0x00, 0x00, 0x00, 0x11, 0x74, 0xF6, 0xA9, 0x71, 0x33, 0xD6, 0x70, 0x01, 0x07, 0x73, 0xF9, 0xF5, 0x10, 0x00, 0x00 }));
            mockSearchResultAttributeCollection
                .Setup(mock => mock["mail"])
                    .Returns(new DirectoryAttribute("mail", new byte[] { 0x01, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x15, 0x00, 0x00, 0x00, 0x11, 0x74, 0xF6, 0xA9, 0x71, 0x33, 0xD6, 0x70, 0x01, 0x07, 0x73, 0xF9, 0xF5, 0x10, 0x00, 0x00 }));
            mockSearchResultAttributeCollection
                .Setup(mock => mock["cn"])
                    .Returns(new DirectoryAttribute("cn", new byte[] { 0x01, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x15, 0x00, 0x00, 0x00, 0x11, 0x74, 0xF6, 0xA9, 0x71, 0x33, 0xD6, 0x70, 0x01, 0x07, 0x73, 0xF9, 0xF5, 0x10, 0x00, 0x00 }));
            mockSearchResultAttributeCollection
                .Setup(mock => mock["distinguishedName"])
                    .Returns(new DirectoryAttribute("distinguishedName", new byte[] { 0x01, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x15, 0x00, 0x00, 0x00, 0x11, 0x74, 0xF6, 0xA9, 0x71, 0x33, 0xD6, 0x70, 0x01, 0x07, 0x73, 0xF9, 0xF5, 0x10, 0x00, 0x00 }));

            Mock<SearchResultEntry> mockSearchResultEntry = new Mock<SearchResultEntry>();
            mockSearchResultEntry
                .Setup(mock => mock.Attributes)
                    .Returns(mockSearchResultAttributeCollection.Object);

            Mock<SearchResultEntryCollection> mockSearchResultEntryCollection = new Mock<SearchResultEntryCollection>();
            mockSearchResultEntryCollection
                .Setup(mock => mock.Cast<SearchResultEntry>())
                    .Returns(new List<SearchResultEntry>() { mockSearchResultEntry.Object });

            Mock<SearchResponse> mockSearchResponse = new Mock<SearchResponse>();
            mockSearchResponse
                .Setup(reponse => reponse.ResultCode)
                    .Returns(ResultCode.Success);
            mockSearchResponse
                .Setup(response => response.Entries)
                    .Returns(mockSearchResultEntryCollection.Object);

            Mock<DirectoryConnection> mockConn = new Mock<DirectoryConnection>();
            mockConn
                .Setup(conn => conn.SendRequest(It.IsAny<DirectoryRequest>()))
                    .Returns(mockSearchResponse.Object);

            DirectoryConnection connection = new LdapConnection("office.crocusgroup.ru");

            /*
            Mock<DirectoryConnection> mockConnection = new Mock<DirectoryConnection>();
            mockConnection
                .Setup(conn => conn.SendRequest(It.IsAny<DirectoryRequest>()) == new DirectoryResponse() { };
            */

            IUserRepository userRepository = new UserRepository(mapper, connection, mockOptions.Object);
            
            string sid = "S-1-5-21-2851501073-1893086065-4185065217-4341";
            #endregion

            #region act
            UserEntity user = userRepository.GetBySID(sid).Result;
            #endregion

            #region assert
            Assert.AreEqual(sid, user.SID);
            #endregion
        }
    }
}
