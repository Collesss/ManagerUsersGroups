namespace ManagerUsersGroups.WpfUI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private string _emailLoginsFIO = string.Empty;
        private string _logins = string.Empty;
        private string _FIOs = string.Empty;
        private string _emails = string.Empty;
        private string _emailsFormattedForOutlook = string.Empty;
        private string _emailsFormattedForHelp = string.Empty;
        private string _emailsDatabase = string.Empty;

        public string EmailLoginsFIO
        {
            get => _emailLoginsFIO;
            set
            {
                _emailLoginsFIO = value;
                OnPropertyChanged();
            }
        }

        public string Logins
        {
            get => _logins;
            set
            {
                _logins = value;
                OnPropertyChanged();
            }
        }

        public string FIOs
        {
            get => _FIOs;
            set
            {
                _FIOs = value;
                OnPropertyChanged();
            }
        }

        public string Emails
        {
            get => _emails;
            set
            {
                _emails = value;
                OnPropertyChanged();
            }
        }

        public string EmailsFormattedForOutlook
        {
            get => _emailsFormattedForOutlook;
            set
            {
                _emailsFormattedForOutlook = value;
                OnPropertyChanged();
            }
        }

        public string EmailsFormattedForHelp
        {
            get => _emailsFormattedForHelp;
            set
            {
                _emailsFormattedForHelp = value;
                OnPropertyChanged();
            }
        }

        public string EmailsDatabase
        {
            get => _emailsDatabase;
            set
            {
                _emailsDatabase = value;
                OnPropertyChanged();
            }
        }
    }
}
