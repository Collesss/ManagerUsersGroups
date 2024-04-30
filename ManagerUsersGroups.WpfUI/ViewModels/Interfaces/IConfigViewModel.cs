using System.ComponentModel;

namespace ManagerUsersGroups.WpfUI.ViewModels.Interfaces
{
    public interface IConfigViewModel : INotifyPropertyChanged
    {
        ConfigLoginType LoginType { get; set; }

        string Path { get; set; }

        string UserName { get; set; }

        string Password { get; set; }


        bool AuthenticationTypesSecure { get; set; }
        bool AuthenticationTypesEncryptionSecureSocketsLayer { get; set; }
        bool AuthenticationTypesReadonlyServer { get; set; }
        bool AuthenticationTypesAnonymous { get; set; }
        bool AuthenticationTypesFastBind { get; set; }
        bool AuthenticationTypesSigning { get; set; }
        bool AuthenticationTypesSealing { get; set; }
        bool AuthenticationTypesDelegation { get; set; }
        bool AuthenticationTypesServerBind { get; set; }
    }
}
