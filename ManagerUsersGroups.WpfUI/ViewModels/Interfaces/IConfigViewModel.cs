using ManagerUsersGroups.Repository.AD.Options;
using System.ComponentModel;
using System.DirectoryServices;

namespace ManagerUsersGroups.WpfUI.ViewModels.Interfaces
{
    public interface IConfigViewModel : INotifyPropertyChanged
    {
        LoginType LoginType { get; set; }

        string Path { get; set; }

        string UserName { get; set; }

        string Password { get; set; }

        AuthenticationTypes AuthenticationType { get; set; }
    }
}
