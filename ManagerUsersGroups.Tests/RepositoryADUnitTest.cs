using AutoMapper;
using ManagerUsersGroups.Repository.AD.AutoMapperProfiles;
using ManagerUsersGroups.Repository.AD.Implementations;
using ManagerUsersGroups.Repository.AD.Options;
using ManagerUsersGroups.Repository.Entities;
using ManagerUsersGroups.Repository.Interfaces;
using Microsoft.Extensions.Options;
using Moq;

namespace ManagerUsersGroups.Tests
{
    
    [TestClass]
    public class RepositoryADUnitTest
    {

        [TestMethod]
        public void TestGetBySidOk()
        {
            #region arrange
            ADOptions options = new ADOptions 
            {
                AuthenticationTypes = System.DirectoryServices.AuthenticationTypes.None
            };


            Mock<IOptionsSnapshot<ADOptions>> mockOptions = new Mock<IOptionsSnapshot<ADOptions>>();
            mockOptions
                .Setup(opts => opts.Value)
                .Returns(options);

            IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();

            IUserRepository userRepository = new UserRepository(mockOptions.Object, mapper);

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
