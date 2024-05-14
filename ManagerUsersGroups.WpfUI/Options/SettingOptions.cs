using ManagerUsersGroups.WpfUI.ViewModels;

namespace ManagerUsersGroups.WpfUI.Options
{
    public class SettingOptions
    {
        public ConfigLoginType LoginType { get; set; }

        public string Path { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }


        public bool AuthenticationTypesSecure { get; set; }
        public bool AuthenticationTypesEncryptionSecureSocketsLayer { get; set; }
        public bool AuthenticationTypesReadonlyServer { get; set; }
        public bool AuthenticationTypesAnonymous { get; set; }
        public bool AuthenticationTypesFastBind { get; set; }
        public bool AuthenticationTypesSigning { get; set; }
        public bool AuthenticationTypesSealing { get; set; }
        public bool AuthenticationTypesDelegation { get; set; }
        public bool AuthenticationTypesServerBind { get; set; }
    }
}
