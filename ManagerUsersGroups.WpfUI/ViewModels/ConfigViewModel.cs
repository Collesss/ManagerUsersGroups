using AutoMapper;
using ManagerUsersGroups.WpfUI.Options;
using ManagerUsersGroups.WpfUI.ViewModels.Interfaces;
using Microsoft.Extensions.Options;

namespace ManagerUsersGroups.WpfUI.ViewModels
{
    public class ConfigViewModel : BaseViewModel, IConfigViewModel
    {
        private ConfigLoginType _loginType;
        private string _path;
        private string _userName;
        private string _password;

        private bool _authenticationTypesSecure;
        private bool _authenticationTypesEncryptionSecureSocketsLayer;
        private bool _authenticationTypesReadonlyServer;
        private bool _authenticationTypesAnonymous;
        private bool _authenticationTypesFastBind;
        private bool _authenticationTypesSigning;
        private bool _authenticationTypesSealing;
        private bool _authenticationTypesDelegation;
        private bool _authenticationTypesServerBind;

        public ConfigViewModel(IMapper mapper, IOptions<SettingOptions> options) 
        {
            mapper.Map(options.Value, this);
        }


        public ConfigLoginType LoginType
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




        public bool AuthenticationTypesSecure
        {
            get => _authenticationTypesSecure;
            set
            {
                _authenticationTypesSecure = value;
                OnPropertyChanged();
            }
        }

        public bool AuthenticationTypesEncryptionSecureSocketsLayer
        {
            get => _authenticationTypesEncryptionSecureSocketsLayer;
            set
            {
                _authenticationTypesEncryptionSecureSocketsLayer = value;
                OnPropertyChanged();
            }
        }

        public bool AuthenticationTypesReadonlyServer
        {
            get => _authenticationTypesReadonlyServer;
            set
            {
                _authenticationTypesReadonlyServer = value;
                OnPropertyChanged();
            }
        }

        public bool AuthenticationTypesAnonymous
        {
            get => _authenticationTypesAnonymous;
            set
            {
                _authenticationTypesAnonymous = value;
                OnPropertyChanged();
            }
        }

        public bool AuthenticationTypesFastBind
        {
            get => _authenticationTypesFastBind;
            set
            {
                _authenticationTypesFastBind = value;
                OnPropertyChanged();
            }
        }

        public bool AuthenticationTypesSigning
        {
            get => _authenticationTypesSigning;
            set
            {
                _authenticationTypesSigning = value;
                OnPropertyChanged();
            }
        }

        public bool AuthenticationTypesSealing
        {
            get => _authenticationTypesSealing;
            set
            {
                _authenticationTypesSealing = value;
                OnPropertyChanged();
            }
        }

        public bool AuthenticationTypesDelegation
        {
            get => _authenticationTypesDelegation;
            set
            {
                _authenticationTypesDelegation = value;
                OnPropertyChanged();
            }
        }

        public bool AuthenticationTypesServerBind
        {
            get => _authenticationTypesServerBind;
            set
            {
                _authenticationTypesServerBind = value;
                OnPropertyChanged();
            }
        }
    }
}
