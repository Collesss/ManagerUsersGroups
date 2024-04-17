using ManagerUsersGroups.Repository.AD.Options;
using System.DirectoryServices;

namespace ManagerUsersGroups.WpfUI.ViewModel
{
    public class ConfigModelView : BaseViewModel
    {
        private LoginType _loginType;
        private string _path;
        private string _userName;
        private string _password;
        private AuthenticationTypes _authenticationTypes;


        public LoginType LoginType
        {
            get => _loginType; 
            set
            {
                _loginType = value;
                OnPropertyChanged();
            }
        }

        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public AuthenticationTypes AuthenticationTypes
        {
            get => _authenticationTypes;
            set
            {
                _authenticationTypes = value;
                OnPropertyChanged();
            }
        }
    }
}
