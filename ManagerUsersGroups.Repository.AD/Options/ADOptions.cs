using System.DirectoryServices;

namespace ManagerUsersGroups.Repository.AD.Options
{
    public class ADOptions
    {
        public LoginType LoginType { get; set; }

        public string Path { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public AuthenticationTypes AuthenticationTypes { get; set; }
    }
}
