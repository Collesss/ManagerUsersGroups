using ManagerUsersGroups.Repository.AD.Options;
using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace ManagerUsersGroups.WpfUI.ViewModel
{
    public class ConfigModelView : BaseViewModel
    {
        private LoginType _loginType;
        private string _path;
        private string _userName;
        private string _password;
        private AuthenticationTypes _authenticationType;


        public IEnumerable<LoginType> LoginTypes =>
            Enum.GetValues<LoginType>();

        public IEnumerable<AuthenticationTypes> AuthenticationTypes =>
            Enum.GetValues<AuthenticationTypes>();



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

        public AuthenticationTypes AuthenticationType
        {
            get => _authenticationType;
            set
            {
                _authenticationType = value;
                OnPropertyChanged();
            }
        }
    }
}
